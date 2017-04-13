(
    function ($) {
        var msg = $('#msg');

        var bookName = $('#bookName');
        var bookNameMsg = $("#bookNameMsg");
        var bookDesc = $("#bookDesc");
        var bookDescMsg = $("#bookDescMsg");
        var fileInput = $("#fileInput");
        var fileInputMsg = $("#fileInputMsg");

        var btnSave = $("#btnSave");

        $.feggplant = {
            init: function () {                
                //bookName.blur(bookNameBlur);
                //bookDesc.blur(bookDescBlur);

                btnSave.click(submit);
            }
        };

        function showMsg(msg) {
            $("#msg").addClass('alert-info');
            $("#msg").removeClass('alert-danger');
            $("#msg").removeClass('hidden');
            $("#msg span").html(msg);
        }

        function showDanger(msg) {
            $("#msg").addClass('alert-danger');
            $("#msg").removeClass('alert-info');
            $("#msg").removeClass('hidden');
            $("#msg span").html(msg);
        }

        bookNameBlur = function () {
            if (!bookName.val() || bookName.val().trim() == '') {
                bookNameMsg.removeClass('hidden');
                bookNameMsg.html('图书名称不能为空');
                return false;
            }

            var maxSize = 50;
            if (bookName.val() && bookName.val().length > maxSize) {
                bookNameMsg.removeClass('hidden');
                bookNameMsg.html('图书名称请小于{0}个字符'.replace('{0}', maxSize));
                return false;
            }

            bookNameMsg.addClass('hidden');
            bookNameMsg.html('');
            return true;
        }

        bookDescBlur = function () {
            //if (!bookDesc.val() || bookDesc.val().trim() == '') {
            //    bookDescMsg.removeClass('hidden');
            //    bookDescMsg.html('网址不能为空');
            //    return false;
            //}

            var maxSize = 100;
            if (bookDesc.val() && bookDesc.val().length > maxSize) {
                bookDescMsg.removeClass('hidden');
                bookDescMsg.html('描述请小于{0}个字符'.replace('{0}', maxSize));
                return false;
            }
            
            bookDescMsg.addClass('hidden');
            bookDescMsg.html('');
            return true;
        };        

        submit = function () {
            btnSave.attr('disabled', "true");
            btnSave.val('提交中...');

            if (!bookNameBlur() | !bookDescBlur()) {
                btnSave.removeAttr('disabled');
                btnSave.val('保存');
                return;
            }

            $('form').submit();
            //var data = $('form').serialize();
            //console.log(data);
            //$.post('/Book/AddBook', data, function (result) {
            //    if (result.code == 1) {
            //        showMsg("保存成功");
            //    } else {
            //        showDanger(result.msg);
            //        btnSave.removeAttr('disabled');
            //        btnSave.val('保存');
            //    }                            
            //});

        };       
    }
)(jQuery);

$(function () {
    $.feggplant.init();
});