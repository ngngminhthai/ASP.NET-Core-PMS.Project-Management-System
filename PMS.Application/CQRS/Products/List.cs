using MediatR;
using System.Collections.Generic;
using System.Diagnostics;


namespace PMS.Application.Products
{
    public class List
    {
        public class Query : IRequest<List<Activity>> { }


    }
}