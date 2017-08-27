using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanBreak.Common.Migrations;
using Microsoft.Owin;

namespace CleanBreak.Integration.Owin
{
    public class CleanBreakOwinMiddleware : OwinMiddleware
    {
        private readonly IVersionProvider _versionProvider;
        private readonly MigrationManager _migrationManager;
        private readonly ICache _cache = new DefaultInMemoryCache();

        public CleanBreakOwinMiddleware(OwinMiddleware next, IMigrationLoader migrationLoader, IVersionProvider versionProvider) : base(next)
        {
            _versionProvider = versionProvider;
            _migrationManager = new MigrationManager(migrationLoader);
        }

        public override async Task Invoke(IOwinContext context)
        {
            IComparable version = _versionProvider.GetVersion(context);
            await migrateRequest(context, version);
            await migrateResponse(context, version);
        }

        private async Task migrateResponse(IOwinContext context, IComparable version)
        {
            OwinMigrationKey migrationKey = new OwinMigrationKey()
            {
                Direction = DataDirection.Outgoing,
                Method = context.Request.Method,
                Uri = context.Request.Uri
            };

            bool applied;
            if (_cache.TryGet(migrationKey.GetStringKey(), out applied))
            {
                if (!applied)
                {
                    return;
                }
            }

            var owinResponse = context.Response;
            var owinResponseStream = owinResponse.Body;
            var responseBuffer = new MemoryStream();
            context.Response.Body = responseBuffer;
            await Next.Invoke(context);

            string responseJsonBody = "";
            responseBuffer.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(responseBuffer))
            {
                responseJsonBody = await reader.ReadToEndAsync();
            }

            var migrationData = new OwinMigrationData() {Body = responseJsonBody};
            _cache[migrationKey.GetStringKey()] = _migrationManager.Migrate(migrationKey, migrationData, version, MigrationDirection.Backward);
            var newResultContent = new StringContent(migrationData.Body, Encoding.UTF8, "application/json");
            var customResponseStream = await newResultContent.ReadAsStreamAsync();
            await customResponseStream.CopyToAsync(owinResponseStream);

            owinResponse.ContentLength = customResponseStream.Length;
            owinResponse.Body = owinResponseStream;
        }

        private async Task migrateRequest(IOwinContext context, IComparable version)
        {
            var migrationKey = new OwinMigrationKey()
            {
                Direction = DataDirection.Incoming,
                Method = context.Request.Method,
                Uri = context.Request.Uri
            };


            bool applied;
            if (_cache.TryGet(migrationKey.GetStringKey(), out applied))
            {
                if (!applied)
                {
                    return;
                }
            }

            var request = context.Request;
            string jsonBody = "";
            using (StreamReader reader = new StreamReader(context.Request.Body))
            {
                jsonBody = await reader.ReadToEndAsync();
            }

            var migrationData = new OwinMigrationData() {Body = jsonBody};
            _cache[migrationKey.GetStringKey()] = _migrationManager.Migrate(migrationKey, migrationData, version, MigrationDirection.Forward);
            var content = new StringContent(migrationData.Body, Encoding.UTF8, "application/json");
            request.Body = await content.ReadAsStreamAsync();
        }
    }
}
