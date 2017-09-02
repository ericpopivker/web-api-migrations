using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanBreak.Common.Versions
{
	public class VersionManager
	{
		private readonly IVersionLoader _versionLoader;
		private readonly IVersionFilter _versionFilter;
		private readonly VersionWrapper[] _versions;

		public VersionManager(IVersionLoader versionLoader, IVersionFilter versionFilter)
		{
			_versionLoader = versionLoader;
			_versionFilter = versionFilter;
			_versions = _versionLoader.Load().OrderBy(s => s.Number).ToArray();
		}

		public bool UpgradeData(object data, IComparable currentVersion)
		{
			IEnumerable<VersionWrapper> versionPipline = _versions.SkipWhile(s => s.Number.CompareTo(currentVersion) <= 0);
			bool changed = false;
			foreach (var versionWrapper in versionPipline)
			{
				if (!_versionFilter.FilterUpgrade(data, versionWrapper))
				{
					continue;
				}
				changed |= versionWrapper.Version.Upgrade(data);
			}
			return changed;
		}

		public bool DowngradeData(object data, IComparable currentVersion)
		{
			IEnumerable<VersionWrapper> versionPipline = _versions.SkipWhile(s => s.Number.CompareTo(currentVersion) <= 0).Reverse();
			bool changed = false;
			foreach (var versionWrapper in versionPipline)
			{
				if (!_versionFilter.FilterDowngrade(data, versionWrapper))
				{
					continue;
				}
				changed |= versionWrapper.Version.Downgrade(data);
			}
			return changed;
		}
	}
}
