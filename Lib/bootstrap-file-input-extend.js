; (function ($) {
    'use strict';

    //MyFileInput插件
    function MyFileInput($el, options) {
        //保存参数
        this.options = $.extend(true, {}, this.DEFAULT, options);
        this.$el = $el;
        //执行初始化
        this.Init();
    }

    //默认值
    MyFileInput.prototype.DEFAULT = {
        language: 'zh', //设置语言
        uploadUrl: null, //上传的地址
        allowedFileExtensions: ['jpg', 'gif', 'png'],//接收的文件后缀
        showUpload: true, //是否显示上传按钮
        showCaption: false,//是否显示标题
        browseClass: "btn btn-primary", //按钮样式     
        dropZoneEnabled: true,//是否显示拖拽区域
        dropZoneTitle: '拖拽文件到这里 …<br />尺寸不超过1024*768',
        minImageWidth: 50, 
        minImageHeight: 50,//图片的最小高度
        maxImageWidth: 1024,//图片的最大宽度
        maxImageHeight: 768,//图片的最大高度
        maxFileSize: 2048,//单位为kb，如果为0表示不限制文件大小
        minFileCount: 0,  //最少文件数量
        maxFileCount: 10,  //最多文件数
        //enctype: 'multipart/form-data',  //使用编码方式
        validateInitialCount: true,  //启用验证技术
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",  //上传文件按钮使用的图标;
        msgFilesTooMany: "选择上传的文件数量({n}) 不能超过允许的最大数值{m}！",  //错误提示            
        //theme: 'explorer',  //样式前缀
        overwriteInitial: false,
        initialPreviewAsData: true,  //预览初始化的数据
        fileuploadedCallback:null//上传完成之后的回调函数
    };

    //初始化fileinput
    MyFileInput.prototype.Init = function () {
        //当前对象
        var $this = this.$el;
        //上传文件的参数
        var options = this.options;
        //初始化上传控件的样式
        $this.fileinput(this.options);

        //导入文件上传完成之后的事件
        $this.on('fileuploaded', function (event, data, id, index) {
            if (options.fileuploadedCallback && typeof options.fileuploadedCallback === 'function') {
                options.fileuploadedCallback(event, data, id, index);
            }
        });
    };

    //设置到jquery的扩展接口供外部访问
    $.fn.bootstrapFileInput = function (options) {
        //遍历传入的对象数组
        this.each(function () {
            //当前对象
            var $this = $(this);
            //检测是否已经初始化过
            //没有就创建一个新的FileInput对象
            var oFile = $this.data('my.file.input') || new MyFileInput($this, options);
            //缓存到html元素中
            $this.data('my.file.input', oFile);
            //直接返回新的对象
            return oFile;
        });
    }
}(jQuery));