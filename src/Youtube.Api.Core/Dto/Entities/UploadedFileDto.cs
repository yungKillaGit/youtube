﻿using System;
using System.Collections.Generic;

namespace Youtube.Api.Core.Dto.Entities
{
    public class UploadedFileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string ContentType { get; set; }

        public virtual ImageDto Image { get; set; }
        public virtual VideoDto Video { get; set; }
    }
}
