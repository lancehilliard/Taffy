using System.Web;
using System.Web.SessionState;
using Taffy.Data;

namespace Taffy.Memory {
    public class UserCache : IUserCache {
        readonly HttpSessionState _session = HttpContext.Current.Session;
        private const string TaffyFeedOpmlFileKey = "TaffyFeedOpmlFile";

        public TaffyFeedOpml TaffyFeedOpml {
            get {
                return _session[TaffyFeedOpmlFileKey] as TaffyFeedOpml;
            }
            set {
                _session[TaffyFeedOpmlFileKey] = value;
            }
        }
    }

    public interface IUserCache {
        TaffyFeedOpml TaffyFeedOpml { get; set; }
    }
}