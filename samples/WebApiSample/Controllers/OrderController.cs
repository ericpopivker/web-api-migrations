using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSample.Dtos;

namespace WebApiSample.Controllers
{
	public class OrderController : ApiController
	{
		private List<OrderDto> _orders = new List<OrderDto>()
		{
			new OrderDto()
			{
				Id = 2
			}
		};

		// GET: api/Order
		public IEnumerable<OrderDto> Get()
		{
			return _orders;
		}

		// GET: api/Order/5
		public OrderDto Get(int id)
		{
			return _orders.FirstOrDefault();
		}

		// POST: api/Order
		public void Post(OrderDto value)
		{

		}

		// PUT: api/Order/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Order/5
		public void Delete(int id)
		{
		}
	}
}
