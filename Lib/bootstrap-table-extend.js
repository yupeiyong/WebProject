(function ($) {
    'use strict';

    var options = {
        cache: false,
        method: 'get',
        dataType: 'json',
        striped: true,
        checkboxHeader: true,
        clickToSelect: true,
        sortable: true,
        sortOrder: 'desc',
        queryParamsType: 'limit',
        pagination: true,
        pageSize: 20,
        pageNumber: 1,  //当期页码
        pageList: [10, 25, 50, 100, 200, 500],  //每页记录数,下拉列表
        paginationFirstText: '首页',  //首页
        paginationPreText: '上一页',  //上一页
        paginationNextText: '下一页',  //下一页
        paginationLastText: '末页',  //末页
        showToggle: false,   //是否显示右上角的名片格式切换按钮
        smartDisplay: true,  //是否用卡片方式显示
        cardView: false,   //名片格式 //$(window).width() < 800
        showHeader: true,  //显示表头
        showFooter: false,  //隐藏表尾
        showColumns: false,  //不显示隐藏列
        showRefresh: false,  //显示刷新按钮
        singleSelect: true,  //复选框只能选择一条记录
        search: false,  //是否显示右上角的搜索框
        sidePagination: "server",  //表格分页方式
        silentSort: true,  //默认排序
        idStartOne: false,  //序号是否从1开始
        responseHandler: function (res) {
            //添加属性映射
            res.success = res.IsSuccess || res.success;
            res.msg = res.Message || res.msg;
            res.total = res.Total || res.total || 0;
            var rows = res.rows = res.data = res.Data || res.data; //返回的数据
            //之前已经有的记录数
            var iCount = this.idStartOne ? 0 : (parseInt(this.pageNumber || 1) - 1) * parseInt(this.pageSize || 0);
            //设置序号
            for (var i = 0, iMax = rows.length; i < iMax ; i++) {
                rows[i]._id = i + 1 + iCount;
            }
            //返回数据
            return res;
        },
        searchButton: ''
    };
    $.fn.extendBootstrapTable = function (params) {
        var tableOptions = $.extend(true, {}, options, params);

        var $this = $(this);
        var $table = $this.bootstrapTable(tableOptions);

        if (tableOptions.searchButton && tableOptions.searchButton.length > 0) {
            $(tableOptions.searchButton).off('click').on('click',
                function () {
                    var $btn = $(this);
                    var $searchForm = $btn.closest('form');
                    var queryData = $searchForm.serializeArray();

                    var queryParam = {};
                    for (var i = 0, len = queryData.length; i < len; i++) {
                        var item = queryData[i];
                        queryParam[item.name] = item.value;
                    }

                    $table.bootstrapTable('refresh',
                    {
                        query: queryParam
                    });
                    return false;
                });
        }
        return $table;
    }
})(jQuery);