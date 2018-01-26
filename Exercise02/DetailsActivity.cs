﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Exercise02.Api;
using Exercise02.Models;

namespace Exercise02
{
    [Activity(Label = "Details", Theme = "@android:style/Theme.Material.Light.DarkActionBar")]
    public class DetailsActivity : Activity
    {
        private List<TextView> textViews;

        public List<string> TextViewValues
        {
            set
            {
                var index = 0;

                value.ForEach(x =>
                {
                    textViews[index].Text = x;
                    index++;
                });
            }
        }

        private string reposUrl;

        public Detail Detail
        {
            set
            {
                reposUrl = value.ReposUrl;

                TextViewValues = new List<string>()
                {
                    "Blog: " + value.Blog,
                    "Company: " + value.Company,
                    "Email: " + value.Email,//?.ToString() ?? "",
                    "Location: " + value.Location,
                    "Name: " + value.Name
                };
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Details);

            textViews = new List<int>()
            {
                Resource.Id.tv_blog,
                Resource.Id.tv_company,
                Resource.Id.tv_email,
                Resource.Id.tv_location,
                Resource.Id.tv_name
            }.Select(x => FindViewById<TextView>(x)).ToList();

            var url = Intent.GetStringExtra("url");
            Detail = GitHub.gitHub.GetDetail(url);

            var adapter = new ArrayAdapter<string>(this, Resource.Layout.Repository, GitHub.gitHub.GetRepositories(reposUrl));
            FindViewById<ListView>(Resource.Id.lv_repository).Adapter = adapter;
        }
    }
}