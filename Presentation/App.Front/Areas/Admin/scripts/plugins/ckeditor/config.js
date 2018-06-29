/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function(config) {
    // Define changes to default configuration here. For example:
    config.skin = "moonocolor";
    //config.skin = 'office2013';
    config.language = "vi";
    // config.uiColor = '#AADC6E';
    config.extraPlugins = "youtube";
    config.youtube_width = "640";
    config.ForcePasteAsPlainText = true;
    config.youtube_height = "480";
    config.filebrowserImageUploadUrl = "http://" + window.location.host + "/Admin/Utility/Upload";
};
