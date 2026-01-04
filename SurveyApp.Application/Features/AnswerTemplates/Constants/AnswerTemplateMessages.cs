using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.AnswerTemplates.Constants;
public static class AnswerTemplateMessages
{
	public const string AnswerTemplateDeleted = "Cevap şablonu başarıyla silindi.";
	public const string AnswerTemplateUpdated = "Cevap şablonu başarıyla güncellendi.";
	public const string AnswerTemplateCreated = "Cevap şablonu başarıyla eklendi.";
	public const string AnswerTemplateInUsed = "Bu cevap şablonu silinemez, sorularda kullanılmıştır.";
	public const string AnswerTemplateNotFound = "Cevap şablonu bulunamadı.";
	public const string AnswerTemplateOptioncount = "Şık sayısı 2 ile 4 arasında olmalıdır.";
	public const string AnswerTemplateOptionMatch = "Şık sayısı ile girilen seçenekler uyuşmuyor.";
}