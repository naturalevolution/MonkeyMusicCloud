﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using PixelLab.Common;
using Contract = PixelLab.Contracts.Contract;


namespace MonkeyMusicCloud.Client.Views.CustomControls.OrgTree
{
    public abstract class OrgItem : Changeable
    {
        protected OrgItem(string name, IEnumerable<OrgItem> children)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));

            if (children == null)
            {
                children = Enumerable.Empty<OrgItem>();
            }

            Name = name;
            m_children = new ReadOnlyCollection<OrgItem>(children.ToArray());
            m_children.ForEach(child => child.SetParent(this));
            m_isVisible = true;
        }

        public string Name { get; private set; }

        public bool IsVisible
        {
            get { return m_isVisible; }
            set
            {
                UpdateProperty("IsVisible", ref m_isVisible, value);
            }
        }

        public bool IsExpanded
        {
            get { return m_isExpanded; }
            set
            {
                UpdateProperty("IsExpanded", ref m_isExpanded, value);
            }
        }

        public OrgItem Parent { get { return m_parent; } }

        public ReadOnlyCollection<OrgItem> Children
        {
            get { return m_children; }
        }

        public void RequestExpand()
        {
            if (m_parent != null)
            {
                m_parent.IsExpanded = true;
                m_parent.RequestExpand();
            }
        }

        public override string ToString()
        {
            return String.Format("OrgItem: '{0}'", Name);
        }

        #region private instance impl

        private void SetParent(OrgItem parent)
        {
            m_parent = parent;
        }

        private bool m_isVisible;
        private bool m_isExpanded;
        private readonly ReadOnlyCollection<OrgItem> m_children;
        private OrgItem m_parent;

        #endregion

        public static ICommand ToggleVisibilityCommand { get { return s_toggleVisibilityCommand; } }

       
        private static readonly ICommand s_toggleVisibilityCommand =
            new DelegateCommand<OrgItem>(oi =>
            {
                oi.IsVisible = !oi.IsVisible;
                if (oi.IsVisible)
                {
                    oi.RequestExpand();
                }
            });
    }

    public class Album : OrgItem
    {
        public Album(string name, ObservableCollection<OrgItem> children) : base(name, children) { }
    }

    public class Artist : OrgItem
    {
        public Artist(string name, ObservableCollection<OrgItem> children) : base(name, children) { }
    }
}
