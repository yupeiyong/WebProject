

/// <reference path="jquery-2.1.4.min.js" />


function AddFavorite(sUrl, sTitle) {
    try {
        if (document.all) {
            window.external.addFavorite(sUrl, sTitle);
        } else {
            window.sidebar.addPanel(sTitle, sUrl, "");
        }
    } catch (e) {
        alert("加入收藏失败，请使用Ctrl+D进行添加");
    }
}


// 生成图片的完整url
function GenerateImageUrl(url, filename, width, height) {
    if (filename) {
        var parts = filename.split(".");
        if (parts.length === 2) {
            url = url + parts[0] + "_" + width + "-" + height + "," + parts[1];
        }
    } else {
        url = url + "null_" + width + "-" + height;
    }

    return url;
}


// 生成图片的完整url
function GenerateDefaultImageUrl(filename, width, height) {
    var url = "/FilePool/Get/";

    return GenerateImageUrl(url, filename, width, height);
}


function GetMaxLengthString(str, maxLength) {
    if (!str) {
        return null;
    }
    if (typeof str !== "string") {
        return null;
    }

    var result;
    if (str.length > maxLength) {
        result = str.substr(0, maxLength) + "...";
    } else {
        result = str;
    }

    return result;
}


//function FormatJsonDate(jsonDate) {
//    var date = new Date(parseInt(jsonDate.substr(6)));
//    //var newDate = dateFormat(jsonDate, "mm/dd/yyyy");
//    return date.toDateString();
//}


function FormatJsonDate(dateStr) {

    if (dateStr === null || dateStr === undefined || dateStr.length < 16) {
        return null;
    }

    var m, day;

    var d = new Date(parseInt(dateStr.substr(6)));
    m = d.getMonth() + 1;
    if (m < 10)
        m = "0" + m;
    if (d.getDate() < 10)
        day = "0" + d.getDate();
    else
        day = d.getDate();
    return (d.getFullYear() + "-" + m + "-" + day);
}


function JSONDateWithTime(dateStr) {
    var d = new Date(parseInt(dateStr.substr(6)));
    var m, day;
    m = d.getMonth() + 1;
    if (m < 10)
        m = "0" + m;
    if (d.getDate() < 10)
        day = "0" + d.getDate();
    else
        day = d.getDate();
    var formattedDate = m + "/" + day + "/" + d.getFullYear();
    var hours = (d.getHours() < 10) ? "0" + d.getHours() : d.getHours();
    var minutes = (d.getMinutes() < 10) ? "0" + d.getMinutes() : d.getMinutes();
    var formattedTime = hours + ":" + minutes + ":" + d.getSeconds();
    formattedDate = formattedDate + " " + formattedTime;
    return formattedDate;
}


function FormDataToObject(formData) {
    var data = {};
    for (var i in formData) {
        if (formData.hasOwnProperty(i)) {
            var row = formData[i];
            data[row["name"]] = row["value"];
        }
    }
    return data;
}


//渲染面包屑导航
function RenderBreadcrumbPanel(url, formData) {
    $.post(url, formData, function(data) {
        if (!data.Success) {
            $.messager.show({
                timeout: 1000 * 7,
                title: data.Title,
                msg: data.Message
            });
            return;
        }

        var container = $("#BreadcrumbPanel .breadcrumb");
        var template = $(container).children(".HiddenTemplate");
        var reserved = $(container).children(".Reserved");
        $(container).empty().append(reserved);

        for (var i in data.rows) {
            if (data.rows.hasOwnProperty(i)) {
                var dom = $(template).clone().removeClass("HiddenPattern").removeClass("Reserved").removeAttr("style");
                var row = data.rows[i];
                if (row.ClassNames) {
                    var classNames = row.ClassNames.join(" ");
                    $(dom).addClass(classNames);
                    $(dom).text(row.Title);
                } else {
                    $(dom).children("a").text(row.Title);
                    $(dom).children("a").attr("href", row.Url);
                }
                $(template).before(dom);
            }
        }
    }, "json");
}


function RefreshBaseInfo() {

    var url = "/BaseInfo/GetAllJson";

    $.post(url, null, function(data) {

        if (!data.Success) {
            $.messager.show({
                timeout: 1000 * 7,
                title: "查询基础信息",
                msg: data.Message
            });
        }

        $("span.BaseInfo.ServiceItemTotalCount").text(data.ServiceItemTotalCount);

        $("span.BaseInfo.SiteShortName").text(data.SiteShortName);
        $("span.BaseInfo.SiteLongName").text(data.SiteLongName);

    });

}


function RenderServiceItemList(container, brands) {

    var reserved = $(container).children(".Reserved");
    $(container).empty().append(reserved);

    var pattern = $(container).find(".Item.HiddenPattern");

    for (var i in brands) {
        if (brands.hasOwnProperty(i)) {
            var brand = brands[i];
            var dom = $(pattern).clone().removeAttr("style").removeClass("HiddenPattern").removeClass("Reserved");

            var detailsUrl = "/ServiceItem/" + brand.Id;
            $(dom).attr("href", detailsUrl);
            $(dom).attr("title", brand.ServiceItemName + "(" + brand.GenreNumber + ")--" + brand.OwnerName);

            var imageUrl = GenerateDefaultImageUrl(brand.ServiceItemImage, 100, 100);
            $(dom).find(".ServiceItemImage").attr("src", imageUrl);
            $(dom).find("a.Details").attr("href", detailsUrl);

            $(dom).attr("data-options", JSON.stringify(brand));

            $(pattern).before(dom);
        }
    }

}


function changeFullDateFormat(jsondate) {
    if (jsondate == null || jsondate === "")
        return null;

    jsondate = jsondate.replace("/Date(", "").replace(")/", "");
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
    } else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
    }

    var date = new Date(parseInt(jsondate, 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    var hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    var minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

    return date.getFullYear()
        + "-"
        + month
        + "-"
        + currentDate
        + " "
        + hour
        + ":"
        + minute
        + ":"
        + second;
}


function changeSimpleDateFormat(jsondate) {
    if (jsondate == null || jsondate === "")
        return null;

    jsondate = jsondate.replace("/Date(", "").replace(")/", "");
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
    } else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
    }

    var date = new Date(parseInt(jsondate, 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();

    return date.getFullYear()
        + "-"
        + month
        + "-"
        + currentDate;
}


function getLocalDate() {
    var myDate = new Date();
    var year = myDate.getFullYear() + "年"; //年
    var month = myDate.getMonth() + 1 + "月"; //月
    var day = myDate.getDate() + "日"; //日
    var week = myDate.getDay(); //星期

    switch (week) {
    case 0:
        week = "星期日";
        break;
    case 1:
        week = "星期一";
        break;
    case 2:
        week = "星期二";
        break;
    case 3:
        week = "星期三";
        break;
    case 4:
        week = "星期四";
        break;
    case 5:
        week = "星期五";
        break;
    case 6:
        week = "星期六";
        break;
    default:
    }

    return year + month + day + "&nbsp;&nbsp;&nbsp;" + week;
}


var LogPanelContainer = "section#LogPanelContainer";

function RefreshLogPanel() {
    var url = "/Admin/Shared/Log/SearchPartial";
    var dto = { StartIndex: 0, TakeCount: 7, OderBy: "OnLastUpdatedDesc" };
    $.post(url, dto, function(response) {
        $(LogPanelContainer).html(response);
    }, "html");
}


function ShowMessage(component, message) {
    $(component).text(message);
}

//easyui后台菜单栏
function RenderCurrentItem(item) {

    $(item).addClass("CurrentItem");

    $(".panel-header.accordion-header.accordion-header-selected").removeClass("accordion-header-selected");
    $(".panel-body.accordion-body").css("display", "none");

    $(item).parent().parent().parent().find(".panel-header").addClass("accordion-header-selected");
    $(item).parent().parent().parent().find(".panel-body.accordion-body").css("display", "block");
}


(function($) {
    $.getUrlParam = function(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null)
            return unescape(r[2]);
        return null;
    };
})(jQuery);


//弹窗居中
function PopBoxAuto(popbox) {
    var width = $(document.body).width();
    var height = $(window).height();
    var popWidth = $(popbox).width();
    var popHeight = $(popbox).height();
    var left = (width / 2) - (popWidth / 2);
    var top = (height / 2) - (popHeight / 2);

    $(popbox).css("margin-top", top);
    $(popbox).css("margin-left", left);
}

//关闭弹窗
function ClosedPop(item) {
    $(item).parents("section.PopBoxContainer").hide();
    $("#Overly").css("display", "none");
}

Array.prototype.contains = function(value) {
    for (item in this) {
        if (this[item] == value) return true;
    }
    return false;
};

function LoginOut() {
    var url = "/AllUser_LogoutSubmitJson";
    $.post(url, function(data) {
        if (data.Success) {
            location.href = "/";
        }
    });
}


function RenderUserStatusBar(container) {
    var url = "/AllUser_UserStatusBarPartial";
    RenderPartialView(url, null, container);
}


function RenderPartialView(url, formData, container) {
    $.post(url, formData, function(response) {
        $(container).html(response);
    }, "html");
}