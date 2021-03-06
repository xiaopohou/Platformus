﻿// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain.Data.Models
{
  public class Property : IEntity
  {
    public int Id { get; set; }
    public int? ObjectId { get; set; }
    public int MemberId { get; set; }
    public int HtmlId { get; set; }

    public virtual Object Object { get; set; }
    public virtual Member Member { get; set; }
    public virtual Dictionary Html { get; set; }
  }
}