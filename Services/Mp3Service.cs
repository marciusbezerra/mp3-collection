using System;
using System.IO;
using System.Linq;

namespace Mp3Collection.Services
{
    public class Mp3Service
    {
        public TagLib.File LerTags(string arquivo)
        {
            var tfile = TagLib.File.Create(arquivo);
            return tfile;
        }

        public InfoMidia PegaInfoMidia(string arquivo)
        {
            TagLib.File file = LerTags(arquivo);
            InfoMidia infoMidia = new InfoMidia();
            infoMidia.Titulo = file.Tag.Title;
            infoMidia.Artistas = string.Join(" | ", file.Tag.Performers);
            infoMidia.Arquivo = $"music/{Path.GetFileName(arquivo)}";
            var firstPicture = file.Tag.Pictures.FirstOrDefault(p => p.Type == TagLib.PictureType.FrontCover);
            if (firstPicture == null) firstPicture = file.Tag.Pictures.FirstOrDefault();

            if (firstPicture != null)
            {
                var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                var imageStr = $"data:{file.Tag.Pictures[0].MimeType};base64,{Convert.ToBase64String(bin)}";
                infoMidia.ImagemCapa = imageStr;
            }

            return infoMidia;
        }
    }

    public class InfoMidia
    {
        public string Titulo { get; set; }
        public string Artistas { get; set; }
        public string ImagemCapa { get; set; }
        public string Arquivo { get; set; }
    }
}
