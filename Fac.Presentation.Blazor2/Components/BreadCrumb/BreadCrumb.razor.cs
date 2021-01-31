using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Fac.Presentation.Blazor2.Components.BreadCrumb
{
    public partial class BreadCrumb
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Parameter]
        public List<BreadCrumbItem> BreadCrumbs { get; set; }

        private List<BreadCrumbItem> _items;

        protected override void OnInitialized()
        {
            _items = new List<BreadCrumbItem>();
            _items.Add(new BreadCrumbItem() { Uri = "/", Name = "", Icon = "home", ParentUri = string.Empty });
            NavigationManager.LocationChanged += OnLocationChanged;
            base.OnInitialized();
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs args)
        {
            var location = RemoveUrl(args.Location);
            var breadCrumb = BreadCrumbs.Find(exp => exp.Uri == location);

            if (breadCrumb != null)
            {
                if (breadCrumb.ParentUri == string.Empty)
                {
                    //RemoveLastBreadCrumb();
                    RemoveAllBreadCrumbs();
                }

                if (location != "/")
                    _items.Add(new BreadCrumbItem() { Uri = args.Location, Name = $" / {breadCrumb.Name}" });

                StateHasChanged();
            }
            else
            {
                // home was clicked
                RemoveAllBreadCrumbs();
                StateHasChanged();
            }
        }

        private string RemoveUrl(string Url)
        {
            string retval = Url;
            var navigation = Url.LastIndexOf("/");

            if (navigation >= 0)
            {
                retval = Url.Substring(navigation);
            }

            return retval;
        }

        private void RemoveAllBreadCrumbs()
        {
            _items.RemoveRange(1, _items.Count - 1);
        }

        private void RemoveLastBreadCrumb()
        {
            if (_items.Count > 1)
            {
                _items.RemoveAt(_items.Count - 1);
            }
        }
    }
}