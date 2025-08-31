using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ArteDaTerraBrasil.Mvc.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-module-tag")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-tag")]
    public class EraseElementTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public EraseElementTagHelper(IHttpContextAccessor contextAccessor, IServiceProvider serviceProvider)
        {
            _contextAccessor = contextAccessor;
            _serviceProvider = serviceProvider;
        }

        [HtmlAttributeName("supress-by-module-tag")]
        public string IdentityModuleNameTag { get; set; }

        [HtmlAttributeName("supress-by-claim-tag")]
        public string IdentityClaimNameTag { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var temAcesso = CustomAuthorization.ValidateClaimsByKey(_contextAccessor.HttpContext, IdentityModuleNameTag, IdentityClaimNameTag);

            if (temAcesso) return;

            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class EraseElementByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public EraseElementByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action)) return;

            output.SuppressOutput();
        }
    }

}
