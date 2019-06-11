using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParsHabr.Models;

namespace ParsHabr.Controllers
{
    public class HomeController : Controller
    {
        private NewsContext db;
        public HomeController(NewsContext context)
        {
            db = context;
        }
        public ActionResult Index(string sourceName, string sortRadio, int page = 1)
        {
            int pageSize = 10;

            List<NewsModel> listNews = new List<NewsModel>();
            var news = db.News.Join(db.Sources,
               p => p.SourceId,
               v => v.Id,
               (p, v) => new
               {
                   Name = p.Headline,
                   Descr = p.Description,
                   SourceName = v.Name,
                   DatePubl = p.PublicationDate.ToString()

               });
            int count = news.Count();

            if ((!String.IsNullOrEmpty(sourceName))&& !String.IsNullOrEmpty(sortRadio))// filter and sort TRUE
            {
                var listSort = news.Where(v => v.SourceName == sourceName);
                if (sortRadio == "date") // sort by date
                {
                    if (sourceName == "all")// filter false
                    {
                        listSort = news.OrderBy(p => p.DatePubl).Skip((page - 1) * pageSize).Take(pageSize);
                    }
                    else //filter true
                    {
                        listSort = news.Where(v => v.SourceName == sourceName).OrderBy(p => p.DatePubl).Skip((page - 1) * pageSize).Take(pageSize);
                    }
                        var sort =listSort.ToList();
                    sort.ForEach(x =>
                    {
                        NewsModel Obj = new NewsModel();
                        Obj.Name = x.Name;
                        Obj.Descr = x.Descr;
                        Obj.SourceName = x.SourceName;
                        Obj.DatePubl = x.DatePubl.ToString();
                        listNews.Add(Obj);
                    });
                    PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                    IndexViewModel viewModel = new IndexViewModel
                    {
                        PageViewModel = pageViewModel,
                        News = listNews,
                        sort=sortRadio,
                        filter=sourceName
                    };

                return View(viewModel);
                }
                else //sort by source
                {
                    if (sourceName == "all")
                    {
                        listSort = news.OrderBy(p => p.SourceName).Skip((page - 1) * pageSize).Take(pageSize);
                    }
                    else
                    {
                        listSort = news.Where(v => v.SourceName == sourceName).OrderBy(p => p.SourceName).Skip((page - 1) * pageSize).Take(pageSize);
                    }
                    var sort = listSort.ToList();
                    sort.ForEach(x =>
                    {
                        NewsModel Obj = new NewsModel();
                        Obj.Name = x.Name;
                        Obj.Descr = x.Descr;
                        Obj.SourceName = x.SourceName;
                        Obj.DatePubl = x.DatePubl.ToString();
                        listNews.Add(Obj);
                    });

                    PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                    IndexViewModel viewModel = new IndexViewModel
                    {
                        PageViewModel = pageViewModel,
                        News = listNews,
                        sort = sortRadio,
                        filter = sourceName
                    };
                    return View(viewModel);

                }
            }
            else //filter and sort FALSE
            {
               var listNotSort=news.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                listNotSort.ForEach(x =>
                {
                    NewsModel Obj = new NewsModel();
                    Obj.Name = x.Name;
                    Obj.Descr = x.Descr;
                    Obj.SourceName = x.SourceName;
                    Obj.DatePubl = x.DatePubl.ToString();
                    listNews.Add(Obj);
                });
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                IndexViewModel viewModel = new IndexViewModel
                {
                    PageViewModel = pageViewModel,
                    News = listNews,
                    sort = sortRadio,
                    filter = sourceName
                };

                return View(viewModel);
            }
        }
    }
}