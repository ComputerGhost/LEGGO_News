using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.TagHelpers
{
    /// <summary>
    /// Upgrades inputs to HTML5 value formats
    /// </summary>
    [HtmlTargetElement("input", Attributes = "[type=date]", TagStructure = TagStructure.WithoutEndTag)]
    public class InputTagHelper : TagHelper
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            switch (Type) {
                case "date":
                    Value = DateTime.TryParse(Value, out var date)
                        ? date.ToString("yyyy-MM-dd")
                        : "";
                    break;
            }

            output.Attributes.SetAttribute("type", Type);
            output.Attributes.SetAttribute("value", Value);
        }
    }
}
