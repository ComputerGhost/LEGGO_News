using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.TagHelpers
{
    [HtmlTargetElement("time")]
    public class TimeTagHelper : TagHelper
    {
        public DateTime? Datetime { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Datetime.HasValue) {
                output.SuppressOutput();
                return;
            }

            var value = Datetime.Value;

            var hasTimeComponent = value.TimeOfDay.TotalSeconds != 0;
            if (hasTimeComponent) {
                output.Attributes.SetAttribute("datetime", value.ToString("u"));
                output.Content.SetContent(value.ToString("MMM d, yyyy a\t h:mm t"));
            }
            else {
                output.Attributes.SetAttribute("datetime", value.ToString("yyyy-MM-dd"));
                output.Content.SetContent(value.ToString("MMM d, yyyy"));
            }
        }
    }
}
