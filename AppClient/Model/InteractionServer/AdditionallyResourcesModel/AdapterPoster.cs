using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace OnlineCinema.Model.InteractionServer.AdditionallyResourcesModel
{
    public class AdapterPoster : Post
    {
        public AdapterPoster(CoverPost post)
        {
            Id = post.Id;
            Name = post.Name;
            Genres = post.Genres;

            MemoryStream byteStream = new MemoryStream(post.Photo); // конверт byte[] в BitmapImage
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = byteStream;
            image.EndInit();
            Photo = image;
        }
    }
}
