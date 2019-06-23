﻿using System;
using System.IO;

namespace Quick.Common.Mvc.ActionResults
{
    public class ResumeFilePathResult : ResumeActionResultBase
    {
        private FileInfo MediaFile { get; set; }

        public ResumeFilePathResult(string fileName) : base(fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException();
            }

            MediaFile = new FileInfo(fileName);
            LastModified = MediaFile.LastWriteTime;
            FileContents = MediaFile.OpenRead();
        }

        public ResumeFilePathResult(string fileName, string etag) : this(fileName)
        {
            EntityTag = etag;
        }
    }
}