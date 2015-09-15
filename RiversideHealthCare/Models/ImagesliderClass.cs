using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class ImagesliderClass
    {
        //creating instance of linq object
        riversideLinqDataContext objImg = new riversideLinqDataContext();

        public IQueryable<Imageslider> getImages()
        {
            var allImages = objImg.Imagesliders.Select(x => x);
            return allImages;
        }

        public Imageslider getImagesById(int _ImageId)
        {
            var allImage = objImg.Imagesliders.SingleOrDefault(x => x.ImageId == _ImageId);
            return allImage;
        }

        //insert
        public bool commitInsert(Imageslider img)
        {
            using (objImg)
            {
                objImg.Imagesliders.InsertOnSubmit(img);
                //commit insert with db
                objImg.SubmitChanges();
                return true;
            }
        }

        //update
        public bool commitUpdate(int _ImageId, string _Name, string _Image, string _Comment, string _Link)
        {
            using (objImg)
            {
                var objUpdateImg = objImg.Imagesliders.Single(x => x.ImageId == _ImageId);
                //setting table cols to new values
                objUpdateImg.Name = _Name;
                objUpdateImg.Image = _Image;
                objUpdateImg.Comment = _Comment;
                objUpdateImg.Link = _Link;
                //commit update against db
                objImg.SubmitChanges();
                return true;
            }
        }

        //delete
        public bool commitDelete(int _ImageId)
        {
            using (objImg)
            {
                var objDeleteImg = objImg.Imagesliders.Single(x => x.ImageId == _ImageId);
                //deleting from table
                objImg.Imagesliders.DeleteOnSubmit(objDeleteImg);
                //committing delete with db
                objImg.SubmitChanges();
                return true;
            }
        }


    }
}