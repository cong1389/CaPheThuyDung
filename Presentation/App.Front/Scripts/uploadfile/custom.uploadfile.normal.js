function InitUploadFile($element) {
	$($element).filer({
		showThumbs: true,
		addMore: true,
		allowDuplicates: false,
		extensions: ["jpg", "png", "gif"],
		captions: {
			button: "Chọn ảnh tải lên",
			feedback: "Chọn ảnh tải lên (Chọn nhiều)",
			feedback2: "Ảnh đã được chọn",
			removeConfirmation: "Bạn thực sự muốn xoá ảnh này?",
			errors: {
				filesLimit: "Ảnh tải lên phải có định dạng:  {{fi-limit}}",
				filesType: "Tệp tin tải lên phải là định dạng ảnh",
				filesSize: "{{fi-name}} quá lớn! Vui lòng tải ảnh có dung lượng {{fi-maxSize}} MB.",
				filesSizeAll:
					"Ảnh đã chọn có dung lượng quá lớn! VUi lòng chọn ảnh có dung lượng {{fi-maxSize}} MB."
			}
		}
	});
}