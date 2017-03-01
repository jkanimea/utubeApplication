using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtubeApplication.Models;
using UtubeApplication.ViewModel;
using System.Data.Entity;
namespace UtubeApplication.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewChannel(ChannelViewModel model)
        {  //logic save channel detail into channel table
            using (var db = new MyTubeEntities())
            {
                var newChannel = new Channel
                {
                    UserId = CurrentSession.CurrentUser.Id,
                    ChannelName = model.Name,
                    ChannelDescription = model.Description,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    CreatedBy = CurrentSession.CurrentUser.Id,
                };
                db.Channels.Add(newChannel);
                db.SaveChanges();
                return Json(new
                {
                    Success = true,
                    ChannelId = newChannel.Id,
                    ChannelName = newChannel.ChannelName
                });
            }
        }
        [HttpGet]
        public ActionResult UploadVideo()
        {

            //upload channel for dropbox 
            using (var db = new MyTubeEntities())
            {
                var allchannel = db.Channels
                                .Where(x => x.UserId == CurrentSession.CurrentUser.Id)
                                .ToList();
                ViewBag.ChannelList = allchannel;
            };



            return View();
        }

        [HttpPost]
        public ActionResult UploadVideo(VideoViewModel model)
        {
            //logic save video data in video table
            using (var db = new MyTubeEntities())
            {
                var newVideo = new Video
                {
                    Title = model.Name,
                    Description = model.Description,
                    URL = model.Url,
                    ChannelId = model.ChannelDropDown,
                    UserId = CurrentSession.CurrentUser.Id,
                    CreatedOn = DateTime.Now,
                    CreatedBy = CurrentSession.CurrentUser.Id,
                    IsActive = true
                };
                db.Videos.Add(newVideo);
                db.SaveChanges();
                return Json(new { Success = true, VideoId = newVideo.Id });
            }
        }


        //Pull the information from the VIdeo Table
        public ActionResult VideoManagement()
        {


            using (var db = new MyTubeEntities())
            {
                var allvideo = db.Videos
                                 .Include(x => x.Channel)
                                  .Include(u => u.User)
                                .Where(x => x.UserId == CurrentSession.CurrentUser.Id)
                                 .ToList();



                return View(allvideo);
            }





        }

        public ActionResult DisplayVideo(int id)
        {
            using (var db = new MyTubeEntities())
            {
                var video = db.Videos.Find(id);

                return View(video);
            }

            
        }



        public ActionResult AddComment(int videoId, string comment)
        {
            using (var db = new MyTubeEntities())
            {
                var commentObj = new Comment
                {
                    Contents = comment,
                    VideoId = videoId,
                    UserId = CurrentSession.CurrentUser.Id,
                    CreatedBy = CurrentSession.CurrentUser.Id,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                };
                db.Comments.Add(commentObj);
                db.SaveChanges();
                return Json(new { Success = true });
            }
        }

        public ActionResult LoadVideoComment(int videoId)
        {
            using (var db = new MyTubeEntities())
            {
                var allComments = db.Comments.Where(x => x.VideoId == videoId).ToList();
                return PartialView("CommentListPartial", allComments);
            }
        }
    }
}