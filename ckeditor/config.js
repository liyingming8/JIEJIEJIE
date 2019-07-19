/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E
    config.filebrowserImageUploadUrl = "/ckeditor/Upload.aspx?type=Images";
    config.image_previewText = ' '; //预览区域显示内容
    //filebrowserImageUploadUrl: '/ckeditor/app/Upload.aspx?type=Images',  // 开启图片上传
   // filebrowserFlashUploadUrl: '/ckeditor/app/Upload.aspx?type=Flash'  //开启FLASH上传
};
