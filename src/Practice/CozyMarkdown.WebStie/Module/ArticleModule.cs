﻿using CozyMarkdown.Data.Models;
using CommonMark;
using Nancy.ModelBinding;
using System.Linq;
using System;

namespace CozyMarkdown.WebStie.Module {

    public class ArticleModule : BaseModule {

        public ArticleModule() {

            Func<ArticlecModel, ArticlecModel> Save = x => {
                if (x.Id == Guid.Empty) {
                    x.Id = Guid.NewGuid();
                    x.CreateDate = DateTime.Now;
                    x.UpdateDate = DateTime.Now;
                    return db.Insert(x);
                }
                else {
                    x = db.Get<ArticlecModel>(y => y.Id == x.Id);
                    x.UpdateDate = DateTime.Now;
                    return db.Update(x);
                }
            };

            Func<ArticlecModel, dynamic> ConvertViewModel = x => {
                var result = new {
                    a = x,
                    html = CommonMarkConverter.Convert(x.Content)
                };
                return result;
            };

            Get["Article/{title}"] = param => {
                string title = param.title;
                var model = db.Get<ArticlecModel>(x => x.Title == title);
                return View["Article/Index", ConvertViewModel(model)];
            };

            Get["Article/Insert"] = param => {
                return View["Article/Insert", new ArticlecModel()];
            };

            Get["Article/{title}/Edit"] = param => {
                string title = param.title;
                var model = db.Get<ArticlecModel>(x => x.Title == title);
                return View["Article/Insert", model];
            };

            Get["Article/{title}/Delete"] = param => {
                string title = param.title;
                var model = db.Get<ArticlecModel>(x => x.Title == title);
                db.Delete<ArticlecModel>(model.Id);
                return View["Home/Index"];
            };

            Post["Article/Insert"] = param => {
                var model = this.Bind<ArticlecModel>();
                model = Save(model);
                if (model == null) {
                    throw new Exception("程序炸了");
                }
                return View["Article/Index", ConvertViewModel(model)];
            };
        }
    }
}
