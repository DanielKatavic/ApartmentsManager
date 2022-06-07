﻿using DataLayer.Dal;
using DataLayer.Managers;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class UpdatePanel : UserControl
    {
        public static Apartment Apartment { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnUpdate_Click(object sender, EventArgs e) 
            => UpdateApartment();

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).DeleteApartment(Apartment.Guid);
            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }

        protected void BtnClose_Click(object sender, EventArgs e) 
            => Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");

        internal void FillPanel()
        {
            FillTagsPanel();
            offcanvasTitle.InnerHtml = $"Edit apartment {Apartment.Name}";
            maxAdults.Value = Apartment.MaxAdults.ToString();
            maxChildren.Value = Apartment.MaxChildren.ToString();
            totalRooms.Value = Apartment.TotalRooms.ToString();
        }

        private void FillTagsPanel()
        {
            int tagId = 0;
            IList<Tag> allTags = ((IRepo)Application["database"]).LoadTags();

            foreach (Tag tag in allTags)
            {
                TagsPanel.Controls.Add(new Literal
                {
                    Text = TagManager.CreateTagCard(tag.Name, tagId++, Apartment.Tags.Contains(tag))
                });
            }
        }

        private void UpdateApartment()
        {
            string status = Request.Form["status"];

            ((IRepo)Application["database"]).UpdateApartment(
                Apartment.Guid,
                int.Parse(maxAdults.Value),
                int.Parse(maxChildren.Value),
                int.Parse(totalRooms.Value),
                status);
            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }
    }
}