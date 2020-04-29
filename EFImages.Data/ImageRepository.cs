using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFImages.Data
{
     public class ImageRepository
    {
        private readonly string _connectionString;
        public ImageRepository( string conn)
        {
            _connectionString = conn;
        }
        public List<Image> GetAllImages()
        {
            using(var context = new ImageContext(_connectionString))
            {
                return context.Images.OrderByDescending(i => i.DatePosted).ToList();
            }
        }
        public Image GetImageById(int id)
        {
            using (var context = new ImageContext(_connectionString))
            {
                return context.Images.FirstOrDefault(i => i.Id == id);
            }
        }
        public void AddImage(Image image)
        {
            using (var context = new ImageContext(_connectionString))
            {
                context.Images.Add(image);
                context.SaveChanges();
            }
        }
        public void LikeImage(Image image)
        {
            using (var context = new ImageContext(_connectionString))
            {
                context.Images.Attach(image);
                context.Entry(image).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        
    }
}
