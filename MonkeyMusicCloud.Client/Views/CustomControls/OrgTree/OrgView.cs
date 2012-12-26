using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using PixelLab.Common;
using PixelLab.Contracts;


namespace MonkeyMusicCloud.Client.Views.CustomControls.OrgTree
{
    public class OrgViewItem
    {
        public OrgViewItem(OrgItem root)
        {
            Contract.Requires<ArgumentNullException>(root != null);
            dataRoot = root;
        }

        public ReadOnlyObservableCollection<OrgViewItem> VisibleChildren
        {
            get
            {
                if (visible == null)
                {
                    visible = new ObservableCollectionPlus<OrgViewItem>(Children.Where(child => child.Data.IsVisible));
                }
                return visible.ReadOnly;
            }
        }

        public ReadOnlyObservableCollection<OrgViewItem> HiddenChildren
        {
            get
            {
                if (hidden == null)
                {
                    hidden = new ObservableCollectionPlus<OrgViewItem>(Children.Where(child => !child.Data.IsVisible));
                }
                return hidden.ReadOnly;
            }
        }

        public ReadOnlyCollection<OrgViewItem> Children
        {
            get
            {
                if (childrenRo == null)
                {
                    if (children == null)
                    {
                        children = dataRoot.Children.Select(child => new OrgViewItem(child)).ToArray();
                        children.ForEach(child => child.Data.AddWatcher("IsVisible", () => RefreshVisible(child)));
                    }
                    childrenRo = new ReadOnlyCollection<OrgViewItem>(children);
                }
                return childrenRo;
            }
        }

        public OrgItem Data
        {
            get { return dataRoot; }
        }

        private void RefreshVisible(OrgViewItem child)
        {
            Debug.Assert(!visible.Contains(child) == hidden.Contains(child));
            if (child.Data.IsVisible && !visible.Contains(child))
            {
                visible.Add(child);
                hidden.Remove(child);
            }
            else if (!child.Data.IsVisible && visible.Contains(child))
            {
                visible.Remove(child);
                hidden.Add(child);
            }
        }

        private OrgViewItem[] children;
        private ReadOnlyCollection<OrgViewItem> childrenRo;
        private ObservableCollectionPlus<OrgViewItem> hidden;
        private ObservableCollectionPlus<OrgViewItem> visible;
        private readonly OrgItem dataRoot;

        public static ObservableCollection<OrgViewItem> GetViewArray(ObservableCollection<OrgItem> items)
        {
            ObservableCollection<OrgViewItem> it = new ObservableCollection<OrgViewItem>(items.Select(item => new OrgViewItem(item)));
            return new ObservableCollection<OrgViewItem>(it);
        }
    }

    public class CountHiddenConverter : SimpleValueConverter<int, Visibility>
    {
        protected override Visibility ConvertBase(int input)
        {
            return input == 0 ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public class ShowContextMenuConverter : SimpleValueConverter<int, bool>
    {
        protected override bool ConvertBase(int input)
        {
            return input > 0;
        }
    }
}
