using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Tavis;

namespace GitHubLib
{
    [LinkRelationType("http://api.github.com/rels/home")]
    public class HomeLink : Link { }

    [LinkRelationType("http://api.github.com/rels/user")]
    public class UserLink : Link
    {
        public string User { get; set; }

        public override HttpRequestMessage CreateRequest()
        {
            SetParameter("user",User);
            return base.CreateRequest();
        }

        public static UserResult InterpretResponse(GithubDocument document)
        {
            var result = new UserResult();
            foreach (var property in document.Properties)
            {
                switch (property.Key)
                {
                    case "login":
                        result.Login = (string) property.Value;
                        break;
                    case "following":
                        result.Following = (int)property.Value;
                        break;
                    case "followers":
                        result.Followers = (int)property.Value;
                        break;
                    case "hireable":
                        result.Hireable = (bool)property.Value;
                        break;
                }
            }

            foreach (var link in document.Links.Values)
            {
                if (link is AvatarLink)
                {
                    result.AvatarLink = (AvatarLink)link;
                }
            }
            return result;
        }
        public class UserResult
        {
            public string Login { get; set; }
            public int Following { get; set; }
            public int Followers { get; set; }
            public bool Hireable { get; set; }
            public AvatarLink AvatarLink { get; set; }
        }
    }

    [LinkRelationType("http://api.github.com/rels/authorizations")]
    public class AuthorizationsLink : Link { }

    [LinkRelationType("http://api.github.com/rels/code_search")]
    public class CodeSearchLink : Link
    {
        public enum SearchSort
        {
            stars,
            forks,
            updated,
            defaultsort
        };

        public string Query { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public SearchSort Sort { get; set; }

        
        public override HttpRequestMessage CreateRequest()
        {
            SetParameter("query", Query);
            if (Page != 0) SetParameter("page", Page);
            if (PerPage != 0) SetParameter("per_page", Page);
            if (Sort != SearchSort.defaultsort) SetParameter("sort",Sort.ToString());

            return base.CreateRequest();
        }

        public CodeSearchResults InterpretResponse(GithubDocument document)
        {
            var results = new CodeSearchResults();

            results.Count = (int)document.Properties["total_count"]; 

            return results;
        }
        public class CodeSearchResults
        {
            public int Count { get; set; }
        }
    }

    [LinkRelationType("http://api.github.com/rels/current_user")]
    public class CurrentUserLink : Link { }

    [LinkRelationType("http://api.github.com/rels/emails")]
    public class EmailsLink : Link { }

    [LinkRelationType("http://api.github.com/rels/emojis")]
    public class EmojisLink : Link { }

    [LinkRelationType("http://api.github.com/rels/events")]
    public class EventsLink : Link { }

    [LinkRelationType("http://api.github.com/rels/feeds")]
    public class FeedsLink : Link { }

    [LinkRelationType("http://api.github.com/rels/following")]
    public class FollowingLink : Link { }

    [LinkRelationType("http://api.github.com/rels/followers")]
    public class FollowersLink : Link { }

    [LinkRelationType("http://api.github.com/rels/gists")]
    public class GistsLink : Link { }
    
    [LinkRelationType("http://api.github.com/rels/avatar")]
    public class AvatarLink : Link { }


   
}
