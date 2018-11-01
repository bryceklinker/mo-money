using System;
using System.Linq.Expressions;
using Identity.Management.Client.ApiResources;
using IdentityServer4.EntityFramework.Entities;

namespace Identity.Management.Server.ApiResources
{
    public static class ApiResourceModelMappers
    {
        public static Expression<Func<ApiResource, ApiResourceModel>> FromEntityExpression = a => new ApiResourceModel
        {
            Name = a.Name,
            DisplayName = a.DisplayName
        };
    }
}