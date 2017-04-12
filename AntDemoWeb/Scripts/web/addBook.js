(
    function ($) {
        var msg = $('#msg');
        var websiteName = $('#bookName');
        var websiteNameMsg = $('#WebsiteNameMsg');
        var websiteUrl = $('#bookDesc');
        var websiteUrlMsg = $('#WebsiteUrlMsg');
        var connectPerson = $('#ConnectPerson');
        var connectPersonMsg = $('#ConnectPersonMsg');
        var connectTel = $('#ConnectTel');
        var connectTelMsg = $('#ConnectTelMsg');
        var connectQQ = $('#ConnectQQ');
        var connectQQMsg = $('#ConnectQQMsg');
        var websiteIntroduction = $('#WebsiteIntroduction');
        var websiteIntroductionMsg = $('#WebsiteIntroductionMsg');

        $.feggplant = {
            init: function () {
                websiteName.blur(websiteNameBlur);
                websiteUrl.blur(websiteUrlBlur);
                connectPerson.blur(connectPersonBlur);
                connectTel.blur(connectTelBlur);
                connectQQ.blur(connectQQBlur);
                websiteIntroduction.blur(websiteIntroductionBlur);

                $('#btnSave').click(submit);
            }
        };

        websiteNameBlur = function () {
            if (!websiteName.val() || websiteName.val().trim() == '') {
                websiteNameMsg.removeClass('newhide');
                websiteNameMsg.html('网站名称不能为空');
                return false;
            }

            var maxSize = 50;
            if (websiteName.val() && websiteName.val().length > maxSize) {
                websiteNameMsg.removeClass('newhide');
                websiteNameMsg.html('名称长度请小于{0}个字符'.replace('{0}', maxSize));
                return false;
            }

            websiteNameMsg.addClass('newhide');
            websiteNameMsg.html('');
            return true;
        }

        websiteUrlBlur = function () {
            if (!websiteUrl.val() || websiteUrl.val().trim() == '') {
                websiteUrlMsg.removeClass('newhide');
                websiteUrlMsg.html('网址不能为空');
                return false;
            }

            var maxSize = 50;
            if (websiteUrl.val() && websiteUrl.val().length > maxSize) {
                websiteUrlMsg.removeClass('newhide');
                websiteUrlMsg.html('网址长度请小于{0}个字符'.replace('{0}', maxSize));
                return false;
            }

            websiteUrlMsg.addClass('newhide');
            websiteUrlMsg.html('');
            return true;
        };

        connectPersonBlur = function () {
            if (!connectPerson.val() || connectPerson.val().trim() == '') {
                connectPersonMsg.removeClass('newhide');
                connectPersonMsg.html('联系人不能为空');
                return false;
            }

            var maxSize = 50;
            if (connectPerson.val() && connectPerson.val().length > maxSize) {
                connectPersonMsg.removeClass('newhide');
                connectPersonMsg.html('联系人长度请小于{0}个字符'.replace('{0}', maxSize));
                return false;
            }

            connectPersonMsg.addClass('newhide');
            connectPersonMsg.html('');
            return true;
        }

        connectTelBlur = function () {
            if (!connectTel.val() || connectTel.val().trim() == '') {
                connectTelMsg.removeClass('newhide');
                connectTelMsg.html('手机号码不能为空');
                return false;
            }

            if (!$.util.isTel(connectTel.val())) {
                connectTelMsg.removeClass('newhide');
                connectTelMsg.html('手机号码格式不正确');
                return false;
            }

            connectTelMsg.addClass('newhide');
            connectTelMsg.html('');
            return true;
        }

        connectQQBlur = function () {
            if (!connectQQ.val() || connectQQ.val().trim() == '') {
                connectQQMsg.removeClass('newhide');
                connectQQMsg.html('QQ不能为空');
                return false;
            }

            if (connectQQ.val() && connectQQ.val().length > 50) {
                connectQQMsg.removeClass('newhide');
                connectQQMsg.html('QQ请小于50个字符');
                return false;
            }

            connectQQMsg.addClass('newhide');
            connectQQMsg.html('');
            return true;
        }

        websiteIntroductionBlur = function () {
            if (!websiteIntroduction.val() || websiteIntroduction.val().trim() == '') {
                websiteIntroductionMsg.removeClass('newhide');
                websiteIntroductionMsg.html('网站介绍不能为空');
                return false;
            }

            var maxSize = 1000;
            if (websiteIntroduction.val() && websiteIntroduction.val().length > maxSize) {
                websiteIntroductionMsg.removeClass('newhide');
                websiteIntroductionMsg.html('网站介绍长度请小于{0}个字符'.replace('{0}', maxSize));
                return false;
            }

            websiteIntroductionMsg.addClass('newhide');
            websiteIntroductionMsg.html('');
            return true;
        };


        submit = function () {
            $('#btnSave').attr('disabled', "true");
            $('#btnSave').val('提交中...');

            if (!websiteNameBlur() | !websiteUrlBlur() | !connectPersonBlur() | !connectTelBlur() | !connectQQBlur() | !websiteIntroductionBlur()) {
                $('#btnSave').removeAttr('disabled');
                $('#btnSave').val('申请');
                return;
            }

            var data = $('form').serializeArray();
            $.post('/FriendlyLinkApply/Index', data, function (data) {
                msg.removeClass('newhide');
                if (data.success) {
                    msg.html('提交成功');

                    // 根据source跳转界面
                    var source = getParameter('source');
                    switch (source) {
                        case 'weiyingxiao':
                            setTimeout(function () { window.location.href = 'http://weixin.qiezzi.com/'; }, 1000);
                            break;
                        case 'yunkouqiangshouye':
                            setTimeout(function () { window.location.href = 'http://clinic.qiezzi.com/'; }, 1000);
                            break;
                        default:
                            setTimeout(function () { window.location.href = '/'; }, 1000);
                    }

                } else {
                    msg.html(data.msg);
                    $('#btnSave').removeAttr('disabled');
                    $('#btnSave').val('申请');
                }
            });
        };
        getParameter = function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }
    }
)(jQuery);

$(function () {
    $.feggplant.init();
});