using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGrappler.Handler
{
    class CustomMenuParams
    {
        public CustomMenuParams(IContextMenuParams para)
        {
            YCoord = para.YCoord;
            XCoord = para.XCoord;
            TypeFlags = para.TypeFlags;
            LinkUrl = para.LinkUrl;
            UnfilteredLinkUrl = para.UnfilteredLinkUrl;
            SourceUrl = para.SourceUrl;
            HasImageContents = para.HasImageContents;
            PageUrl = para.PageUrl;
            FrameCharset = para.FrameCharset;
            FrameUrl = para.FrameUrl;
            MediaType = para.MediaType;
            MediaStateFlags = para.MediaStateFlags;
            SelectionText = para.SelectionText;
            MisspelledWord = para.MisspelledWord;
            DictionarySuggestions = para.DictionarySuggestions;
            IsEditable = para.IsEditable;
            IsSpellCheckEnabled = para.IsSpellCheckEnabled;
            EditStateFlags = para.EditStateFlags;
            IsCustomMenu = para.IsCustomMenu;
            IsPepperMenu = para.IsPepperMenu;
            IsDisposed = para.IsDisposed;
        }

        public int YCoord {get;set;}

        public int XCoord {get;set;}

        public ContextMenuType TypeFlags {get;set;}

        public string LinkUrl {get;set;}

        public string UnfilteredLinkUrl {get;set;}

        public string SourceUrl {get;set;}

        public bool HasImageContents {get;set;}

        public string PageUrl {get;set;}

        public string FrameUrl {get;set;}

        public string FrameCharset {get;set;}

        public ContextMenuMediaType MediaType {get;set;}

        public ContextMenuMediaState MediaStateFlags {get;set;}

        public string SelectionText {get;set;}

        public string MisspelledWord {get;set;}

        public List<string> DictionarySuggestions {get;set;}

        public bool IsEditable {get;set;}

        public bool IsSpellCheckEnabled {get;set;}

        public ContextMenuEditState EditStateFlags {get;set;}

        public bool IsCustomMenu {get;set;}

        public bool IsPepperMenu {get;set;}

        public bool IsDisposed {get;set;}

    }
}
