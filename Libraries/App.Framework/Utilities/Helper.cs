using System;

namespace App.Framework.Utilities
{
    public class Helper
    {
        public class PageInfo
        {
            public readonly int Leave;

            private int _limit;

            private int _page;

            public int Begin
            {
                get
                {
                    int num = (_page - 1) * _limit + 1;
                    return num;
                }
            }

            public int CurrentPage
            {
                get => _page;
                set => _page = value < 1 ? 1 : value;
            }

            public int End => _page * _limit;

            public bool HideJumpBox
            {
                get;
                set;
            }

            public bool HideLimitBox
            {
                get;
                set;
            }

            public int ItemsPerPage
            {
                get => _limit;
                set
                {
                    int num;
                    if (value < 4)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = value > 200 ? 200 : value;
                    }
                    _limit = num;
                }
            }

            public double TotalItems
            {
                get;
                set;
            }

            public int TotalPage => ItemsPerPage > 0 ? (int)Math.Ceiling(TotalItems / ItemsPerPage) : 0;

            public Func<int, string> Url
            {
                get;
                set;
            }

            public PageInfo()
            {
                Url = i => "";
            }

            public PageInfo(int limit, int page) : this(limit, page, 0, false, false, u => string.Empty)
            {
            }

            public PageInfo(int limit, int page, int count, Func<int, string> url) : this(limit, page, count, false, false, url)
            {
            }

            public PageInfo(int limit, int page, int count, bool hlb, bool hjb, Func<int, string> url)
            {
                ItemsPerPage = limit;
                CurrentPage = page;
                TotalItems = count;
                HideLimitBox = hlb;
                HideJumpBox = hjb;
                Url = url;
            }
        }
    }
}