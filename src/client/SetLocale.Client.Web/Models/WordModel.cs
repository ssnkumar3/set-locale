﻿using System.Linq;
using SetLocale.Client.Web.Entities;
using System.Collections.Generic;

namespace SetLocale.Client.Web.Models
{
    public class WordModel : BaseModel
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public bool IsTranslated { get; set; }
        public List<TagModel> Tags { get; set; }
        public List<LanguageModel> Languages { get; set; }
        public List<TranslationModel> Translations { get; set; }
        public int CreatedBy { get; set; }

        public WordModel()
        {
            Tags = new List<TagModel>();
            Languages = new List<LanguageModel>();
            Translations = new List<TranslationModel>();
        }

        public bool IsValidForNew()
        {
            return !string.IsNullOrEmpty(Key)
                   && !string.IsNullOrEmpty(Tag);
        }

        public static WordModel MapEntityToModel(Word entity)
        {
            var model = new WordModel
            {
                Key = entity.Key,
                Description = entity.Description,
                IsTranslated = entity.IsTranslated,
                CreatedBy = entity.CreatedBy
            };

            if (entity.Tags != null
                && entity.Tags.Any())
            {
                foreach (var tag in entity.Tags)
                {
                    model.Tags.Add(new TagModel
                    {
                        Name = tag.Name,
                        UrlName = tag.UrlName
                    });

                    model.Tag += string.Format("{0},", tag);
                }
            }

            if (entity.IsTranslated)
            {
                if (!string.IsNullOrEmpty(entity.Translation_TR))
                {
                    model.Translations.Add(new TranslationModel
                    {
                        Key = entity.Key,
                        Value = entity.Translation_TR,
                        Language = LanguageModel.TR()
                    });

                    model.Languages.Add(LanguageModel.TR());
                }

                if (!string.IsNullOrEmpty(entity.Translation_EN))
                {
                    model.Translations.Add(new TranslationModel
                    {
                        Key = entity.Key,
                        Value = entity.Translation_EN,
                        Language = LanguageModel.EN()
                    });

                    model.Languages.Add(LanguageModel.EN());
                }

                if (!string.IsNullOrEmpty(entity.Translation_AZ))
                {
                    model.Translations.Add(new TranslationModel
                    {
                        Key = entity.Key,
                        Value = entity.Translation_AZ,
                        Language = LanguageModel.AZ()
                    });

                    model.Languages.Add(LanguageModel.AZ());
                }
            }

            return model;
        }
    }
}