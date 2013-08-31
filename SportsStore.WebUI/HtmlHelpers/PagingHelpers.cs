using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.HtmlHelpers
{
    /// <summary>
    /// HtmlHelpers concerned with paging.
    /// </summary>
    public static class PagingHelpers
    {

        /// <summary>
        /// HtmlHelper that builds paging links according to the paging info sent in.
        /// </summary>
        /// <param name="helper">HtmlHelper extension method</param>
        /// <param name="pagingInfo">PagingInfo View Model</param>
        /// <param name="pageUrl">Delegate that takes the page number and returns a url string</param>
        /// <returns>The set of page links in HTML</returns>
        public static MvcHtmlString PageLinks(
            this HtmlHelper helper, 
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            // Build link tags for all the pages
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                }
                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}