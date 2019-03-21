//
// Copyright (c) 2019 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.Capreze.Runtime.Extensions
{

    public static class BitmapExtensions
    {

        public static byte[] ToByteArray(this Bitmap target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            using (var stream = new MemoryStream())
            {
                target.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

    }

}
