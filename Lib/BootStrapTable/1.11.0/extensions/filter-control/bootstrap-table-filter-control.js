/**
 * @author: Dennis Hernández
 * @webSite: http://djhvscf.github.io/Blog
 * @version: v2.1.0
 */

(function ($) {

    "use strict";

    var sprintf = $.fn.bootstrapTable.utils.sprintf,
        objectKeys = $.fn.bootstrapTable.utils.objectKeys;

    var addOptionToSelectControl = function (selectControl, value, text) {
        value = $.trim(value);
        //selectControl = $(selectControl.get(selectControl.length - 1));
        if (!existOptionInSelectControl(selectControl, value)) {
            selectControl.append($("<option></option>")
                .attr("value", value)
                .text($('<div />').html(text).text()));
        }
    };

    var sortSelectControl = function (selectControl) {
        var $opts = selectControl.find('option:gt(0)');
        $opts.sort(function (a, b) {
            a = $(a).text().toLowerCase();
            b = $(b).text().toLowerCase();
            if ($.isNumeric(a) && $.isNumeric(b)) {
                // Convert numerical values from string to float.
                a = parseFloat(a);
                b = parseFloat(b);
            }
            return a > b ? 1 : a < b ? -1 : 0;
        });

        selectControl.find('option:gt(0)').remove();
        selectControl.append($opts);
    };

    var existOptionInSelectControl = function (selectControl, value) {
        var options = selectControl.get(selectControl.length - 1).options;
        if (!options) {
            return false;
        }
        for (var i = 0; i < options.length; i++) {
            if (options[i].value === value.toString()) {
                //The value is not valid to add
                return true;
            }
        }

        //If we get here, the value is valid to add
        return false;
    };

    var fixHeaderCSS = function (that) {
        var $thInners = that.$tableHeader.find('.th-inner');
        //that.$tableHeader.css('height', '77px');
        if (!that.options.filterControl || !that.options.height) {
            that.$tableHeader.css('height', null);
            $thInners.css({
                'margin-top': null,
                'margin-bottom': null
            });
            return;
        }
        //设置th-inner高度
        var maxHeight = 0;
        $thInners.each(function () {
            var height = $(this).height();
            if (maxHeight < height) {
                maxHeight = height;
            }
        });
        $thInners.each(function () {
            var $this = $(this);
            var height = $this.height();
            if (maxHeight > height) {
                var margin = ((maxHeight - height) / 2) + 'px';
                $this.css({
                    'margin-top': margin,
                    'margin-bottom': margin
                });
            }
        });
        //$tableHeader高度
        var heightHeader = maxHeight;
        if (that.options.isSingleAll) {  //单行筛选
            heightHeader += 53;
        } else {
            heightHeader += 79;
        }
        that.$tableHeader.css('height', heightHeader + 'px').parent().css('padding-bottom', (heightHeader - 1) + 'px');
    };

    var getCurrentHeader = function (that) {
        var header = that.$header;
        if (that.options.height) {
            header = that.$tableHeader;
        }

        return header;
    };

    var getCurrentSearchControls = function (that) {
        var searchControls = 'select, input';
        if (that.options.height) {
            searchControls = 'table select, table input';
        }

        return searchControls;
    };

    var getCursorPosition = function (el) {
        if ($.fn.bootstrapTable.utils.isIEBrowser()) {
            if ($(el).is('input')) {
                var pos = 0;
                if ('selectionStart' in el) {
                    pos = el.selectionStart;
                } else if ('selection' in document) {
                    el.focus();
                    var Sel = document.selection.createRange();
                    var SelLength = document.selection.createRange().text.length;
                    Sel.moveStart('character', -el.value.length);
                    pos = Sel.text.length - SelLength;
                }
                return pos;
            } else {
                return -1;
            }
        } else {
            return -1;
        }
    };

    var setCursorPosition = function (el, index) {
        if ($.fn.bootstrapTable.utils.isIEBrowser()) {
            if (el.setSelectionRange !== undefined) {
                el.setSelectionRange(index, index);
            } else {
                $(el).val(el.value);
            }
        }
    };

    var copyValues = function (that) {
        var header = getCurrentHeader(that),
            searchControls = getCurrentSearchControls(that);

        that.options.valuesFilterControl = [];

        header.find(searchControls).each(function () {
            that.options.valuesFilterControl.push(
                {
                    filterFlag: $(this).attr('data-u-filter-flag'),
                    field: $(this).closest('[data-field]').data('field'),
                    value: $(this).val(),
                    position: getCursorPosition($(this).get(0))
                });
        });
    };

    var setValues = function (that) {
        var filterFlag = '',
            field = null,
            result = [],
            header = getCurrentHeader(that),
            searchControls = getCurrentSearchControls(that);

        if (that.options.valuesFilterControl.length > 0) {
            header.find(searchControls).each(function (index, ele) {
                filterFlag = $(this).attr('data-u-filter-flag'),
                field = $(this).closest('[data-field]').data('field');
                result = $.grep(that.options.valuesFilterControl, function (valueObj) {
                    return valueObj.field === field && valueObj.filterFlag === filterFlag;
                });

                if (result.length > 0) {
                    $(this).val(result[0].value);
                    setCursorPosition($(this).get(0), result[0].position);
                }
            });
        }
    };

    var collectBootstrapCookies = function cookiesRegex() {
        var cookies = [],
            foundCookies = document.cookie.match(/(?:bs.table.)(\w*)/g);

        if (foundCookies) {
            $.each(foundCookies, function (i, cookie) {
                if (/./.test(cookie)) {
                    cookie = cookie.split(".").pop();
                }

                if ($.inArray(cookie, cookies) === -1) {
                    cookies.push(cookie);
                }
            });
            return cookies;
        }
    };

    var initFilterSelectControls = function (that) {
        var data = that.options.data,
            itemsPerPage = that.pageTo < that.options.data.length ? that.options.data.length : that.pageTo,

            isColumnSearchableViaSelect = function (column) {
                return column.filterControl && column.filterControl.toLowerCase() === 'select' && column.searchable;
            },

            isFilterDataNotGiven = function (column) {
                return column.filterData === undefined || column.filterData.toLowerCase() === 'column';
            },

            hasSelectControlElement = function (selectControl) {
                return selectControl && selectControl.length > 0;
            };

        var z = that.options.pagination ?
            (that.options.sidePagination === 'server' ? that.pageTo : that.options.totalRows) :
            that.pageTo;

        $.each(that.header.fields, function (j, field) {
            var column = that.columns[$.fn.bootstrapTable.utils.getFieldIndex(that.columns, field)],
                selectControl = $('.bootstrap-table-filter-control-' + escapeID(column.field));

            if (isColumnSearchableViaSelect(column) && isFilterDataNotGiven(column) && hasSelectControlElement(selectControl)) {
                if (selectControl.get(selectControl.length - 1).options.length === 0) {
                    //Added the default option
                    $.fn.bootstrapTable.defaults.filterControlExpose.addOptionToSelectControl(selectControl, '', '--请选择--');
                }

                var uniqueValues = {};
                for (var i = 0; i < z; i++) {
                    //Added a new value
                    var fieldValue = data[i][field],
                        formattedValue = $.fn.bootstrapTable.utils.calculateObjectValue(that.header, that.header.formatters[j], [fieldValue, data[i], i], fieldValue);

                    uniqueValues[formattedValue] = fieldValue;
                }
                for (var key in uniqueValues) {
                    $.fn.bootstrapTable.defaults.filterControlExpose.addOptionToSelectControl(selectControl, uniqueValues[key], key);
                }

                $.fn.bootstrapTable.defaults.filterControlExpose.sortSelectControl(selectControl);
            }
        });
    };

    var escapeID = function (id) {
        return String(id).replace(/(:|\.|\[|\]|,)/g, "\\$1");
    };

    var createControls = function (that, header) {
        var addedFilterControl = false,
            isVisible,
            html,
            timeoutId = 0,
            isSingleAll = true;  //是否全部单行筛选

        $.each(that.columns, function (i, column) {
            isVisible = 'hidden';
            html = [];

            if (!column.visible) {
                return;
            }

            if (!column.filterControl) {
                html.push('<div style="height: 60px;"></div>');
            } else {
                html.push('<div style="height: 60px;margin: 0 2px 2px 2px;" class="filterControl">');

                if (column.filterControl.toLowerCase().substr(-1) === '2') {
                    isSingleAll = false;  //非单行
                }

                var nameControl = column.filterControl.toLowerCase();
                if (column.searchable && that.options.filterTemplate[nameControl]) {
                    addedFilterControl = true;
                    isVisible = 'visible';
                    html.push(that.options.filterTemplate[nameControl](that, column.field, isVisible));
                }
            }

            $.each(header.children().children(), function (i, tr) {
                tr = $(tr);
                if (tr.data('field') === column.field) {
                    tr.find('.fht-cell').append(html.join(''));
                    return false;
                }
            });

            if (column.filterData !== undefined && column.filterData.toLowerCase() !== 'column') {
                var filterDataType = getFilterDataMethod(
                    $.fn.bootstrapTable.defaults.filterControlExpose.filterDataMethods,
                    column.filterData.substring(0, column.filterData.indexOf(':'))
                );
                var filterDataSource, selectControl;

                if (filterDataType !== null) {
                    filterDataSource = column.filterData.substring(column.filterData.indexOf(':') + 1, column.filterData.length);
                    selectControl = $('.bootstrap-table-filter-control-' + escapeID(column.field));

                    $.fn.bootstrapTable.defaults.filterControlExpose.addOptionToSelectControl(selectControl, '', '--请选择--');
                    filterDataType(filterDataSource, selectControl);
                } else {
                    throw new SyntaxError('Error. You should use any of these allowed filter data methods: var, json, url.' + ' Use like this: var: {key: "value"}');
                }

                //var variableValues, key;
                //switch (filterDataType) {
                //    case 'url':
                //        $.ajax({
                //            url: filterDataSource,
                //            dataType: 'json',
                //            success: function (data) {
                //                for (var key in data) {
                //                    $.fn.bootstrapTable.defaults.filterControlExpose.addOptionToSelectControl(selectControl, key, data[key]);
                //                }
                //                $.fn.bootstrapTable.defaults.filterControlExpose.sortSelectControl(selectControl);
                //            }
                //        });
                //        break;
                //    case 'var':
                //        variableValues = window[filterDataSource];
                //        for (key in variableValues) {
                //            $.fn.bootstrapTable.defaults.filterControlExpose.addOptionToSelectControl(selectControl, key, variableValues[key]);
                //        }
                //        $.fn.bootstrapTable.defaults.filterControlExpose.sortSelectControl(selectControl);
                //        break;
                //    case 'json':
                //        variableValues = JSON.parse(filterDataSource);
                //        for (key in variableValues) {
                //            $.fn.bootstrapTable.defaults.filterControlExpose.addOptionToSelectControl(selectControl, key, variableValues[key]);
                //        }
                //        $.fn.bootstrapTable.defaults.filterControlExpose.sortSelectControl(selectControl);
                //        break;
                //}
            }
        });

        if (isSingleAll) {  //单行筛选
            that.options.isSingleAll = isSingleAll;
        }

        if (addedFilterControl) {
            header.children().children().off('keypress');

            header.off('keyup', 'input').on('keyup', 'input', function (event) {
                //检测输入的键
                if ((event.keyCode || event.which) !== 13) {
                    return;
                }
                that.onColumnSearch(event);
                return false;
            }).click(function () {
                return false;
            });

            header.off('change', 'select').on('change', 'select', function (event) {
                that.onColumnSearch(event);
            });

            //header.off('mouseup', 'input').on('mouseup', 'input', function (event) {
            //    var $input = $(this),
            //    oldValue = $input.val();

            //    if (oldValue === "") {
            //        return;
            //    }

            //    setTimeout(function () {
            //        var newValue = $input.val();

            //        if (newValue === "") {
            //            clearTimeout(timeoutId);
            //            timeoutId = setTimeout(function () {
            //                that.onColumnSearch(event);
            //            }, that.options.searchTimeOut);
            //        }
            //    }, 1);
            //});

            if (header.find('.date-filter-control').length > 0) {
                $.each(that.columns, function (i, column) {
                    if (column.filterControl !== undefined && column.filterControl.toLowerCase() === 'datepicker') {
                        header.find('.date-filter-control.bootstrap-table-filter-control-' + column.field).datepicker(column.filterDatepickerOptions)
                            .on('changeDate', function (e) {
                                //Fired the keyup event
                                $(e.currentTarget).keyup();
                            });
                    }
                });
            }
        } else {
            header.find('.filterControl').hide();
        }
    };

    var getDirectionOfSelectOptions = function (alignment) {
        alignment = alignment === undefined ? 'left' : alignment.toLowerCase();

        switch (alignment) {
            case 'left':
                return 'ltr';
            case 'right':
                return 'rtl';
            case 'auto':
                return 'auto';
            default:
                return 'ltr';
        }
    };

    var filterDataMethods =
        {
            'var': function (filterDataSource, selectControl) {
                var variableValues = window[filterDataSource];
                for (var key in variableValues) {
                    $.fn.bootstrapTable.defaults.filterControlExpose.addOptionToSelectControl(selectControl, key, variableValues[key]);
                }
                $.fn.bootstrapTable.defaults.filterControlExpose.sortSelectControl(selectControl);
            },
            'url': function (filterDataSource, selectControl) {
                $.ajax({
                    url: filterDataSource,
                    dataType: 'json',
                    success: function (data) {
                        for (var key in data) {
                            $.fn.bootstrapTable.defaults.filterControlExpose.addOptionToSelectControl(selectControl, key, data[key]);
                        }
                        $.fn.bootstrapTable.defaults.filterControlExpose.sortSelectControl(selectControl);
                    }
                });
            },
            'json': function (filterDataSource, selectControl) {
                var variableValues = JSON.parse(filterDataSource);
                for (var key in variableValues) {
                    $.fn.bootstrapTable.defaults.filterControlExpose.addOptionToSelectControl(selectControl, key, variableValues[key]);
                }
                $.fn.bootstrapTable.defaults.filterControlExpose.sortSelectControl(selectControl);
            }
        };

    var getFilterDataMethod = function (objFilterDataMethod, searchTerm) {
        var keys = Object.keys(objFilterDataMethod);
        for (var i = 0; i < keys.length; i++) {
            if (keys[i] === searchTerm) {
                return objFilterDataMethod[searchTerm];
            }
        }
        return null;
    };

    $.extend($.fn.bootstrapTable.defaults, {
        filterControl: false,
        onColumnSearch: function (field, text) {
            return false;
        },
        filterShowClear: false,
        alignmentSelectControlOptions: undefined,
        filterTemplate: {
            input: function (that, field, isVisible) {
                return sprintf(
                    '<input type="text" class="form-control input-sm bootstrap-table-filter-control-%s" style="width: 100%; visibility: %s" data-u-filter-flag="filter-%s">'
                    , field
                    , isVisible
                    , field
                );
            },
            select: function (that, field, isVisible) {
                return sprintf(
                    '<select class="form-control input-sm bootstrap-table-filter-control-%s" style="width: 100%; visibility: %s" dir="%s" data-u-filter-flag="filter-%s"></select>'
                    , field
                    , isVisible
                    , getDirectionOfSelectOptions(that.options.alignmentSelectControlOptions)
                    , field
                );
            },
            datepicker: function (that, field, isVisible) {
                return sprintf(
                    '<input type="text" class="form-control input-sm date-filter-control bootstrap-table-filter-control-%s" style="width: 100%; visibility: %s" data-u-filter-flag="filter-%s">'
                    , field
                    , isVisible
                    , field
                );
            }
        },
        //internal variables
        valuesFilterControl: [],
        //写扩展时添加的暴露给外部的函数或者变量,以方便做扩展
        filterControlExpose: {
            getDirectionOfSelectOptions: getDirectionOfSelectOptions,
            createControls: createControls,
            copyValues: copyValues,
            setValues: setValues,
            getCurrentSearchControls: getCurrentSearchControls,
            getCurrentHeader: getCurrentHeader,
            filterDataMethods: filterDataMethods,
            addOptionToSelectControl: addOptionToSelectControl,
            sortSelectControl: sortSelectControl
        }
    });

    $.extend($.fn.bootstrapTable.columnDefaults, {
        filterControl: undefined,
        filterData: undefined,
        filterDatepickerOptions: undefined,
        filterSelect2Options: {
            //是否包含x,这个清空按钮
            allowClear: false,
            //取消搜索框
            minimumResultsForSearch: -1
        },
        filterStrictSearch: false,
        filterStartsWithSearch: false
    });

    $.extend($.fn.bootstrapTable.Constructor.EVENTS, {
        'column-search.bs.table': 'onColumnSearch'
    });

    $.extend($.fn.bootstrapTable.defaults.icons, {
        clear: 'glyphicon-trash icon-clear'
    });

    $.extend($.fn.bootstrapTable.locales, {
        formatClearFilters: function () {
            return 'Clear Filters';
        }
    });
    $.extend($.fn.bootstrapTable.defaults, $.fn.bootstrapTable.locales);

    var BootstrapTable = $.fn.bootstrapTable.Constructor,
        _init = BootstrapTable.prototype.init,
        _initToolbar = BootstrapTable.prototype.initToolbar,
        _initHeader = BootstrapTable.prototype.initHeader,
        _initBody = BootstrapTable.prototype.initBody,
        _initSearch = BootstrapTable.prototype.initSearch;

    BootstrapTable.prototype.init = function () {
        //Make sure that the filterControl option is set
        if (this.options.filterControl) {
            var that = this;

            // Compatibility: IE < 9 and old browsers
            if (!Object.keys) {
                objectKeys();
            }

            //Make sure that the internal variables are set correctly
            this.options.valuesFilterControl = [];

            this.$el.on('reset-view.bs.table', function () {
                //Create controls on $tableHeader if the height is set
                if (!that.options.height) {
                    return;
                }

                //Avoid recreate the controls
                if (that.$tableHeader.find('select').length > 0 || that.$tableHeader.find('input').length > 0) {
                    return;
                }
                ////此处有修改调用外部传入的createControls
                //$.fn.bootstrapTable.defaults.filterControlExpose.createControls(that, that.$tableHeader);
                //}).on('post-body.bs.table', function () {
                //fixHeaderCSS(that);
            }).on('post-header.bs.table column-switch.bs.table load-success.bs.table', function () {
                setValues(that);
            }).on('post-header.bs.table', function () {
                fixHeaderCSS(that);
            });
        }
        _init.apply(this, Array.prototype.slice.apply(arguments));
    };

    BootstrapTable.prototype.initToolbar = function () {
        this.showToolbar = this.options.filterControl && this.options.filterShowClear;

        _initToolbar.apply(this, Array.prototype.slice.apply(arguments));

        if (this.options.filterControl && this.options.filterShowClear) {
            var $btnGroup = this.$toolbar.find('>.btn-group'),
                $btnClear = $btnGroup.find('.filter-show-clear');

            if (!$btnClear.length) {
                $btnClear = $([
                    '<button class="btn btn-default filter-show-clear" ',
                    sprintf('type="button" title="%s">', this.options.formatClearFilters()),
                    sprintf('<i class="%s %s"></i> ', this.options.iconsPrefix, this.options.icons.clear),
                    '</button>'
                ].join('')).appendTo($btnGroup);

                $btnClear.off('click').on('click', $.proxy(this.clearFilterControl, this));
            }
        }
    };

    BootstrapTable.prototype.initHeader = function () {
        _initHeader.apply(this, Array.prototype.slice.apply(arguments));

        if (!this.options.filterControl) {
            return;
        }
        //此处有修改调用外部传入的createControls
        $.fn.bootstrapTable.defaults.filterControlExpose.createControls(this, this.$header);
    };

    BootstrapTable.prototype.initBody = function () {
        _initBody.apply(this, Array.prototype.slice.apply(arguments));

        initFilterSelectControls(this);
    };

    BootstrapTable.prototype.initSearch = function () {
        _initSearch.apply(this, Array.prototype.slice.apply(arguments));

        if (this.options.sidePagination === 'server') {
            return;
        }

        var that = this;
        var fcp = $.isEmptyObject(this.filterColumnsPartial) ? null : this.filterColumnsPartial;

        //Check partial column filter
        this.data = fcp ? $.grep(this.data, function (item, i) {
            for (var key in fcp) {
                var thisColumn = that.columns[$.fn.bootstrapTable.utils.getFieldIndex(that.columns, key)];
                var value = item[key];
                for (var key2 in fcp[key]) {
                    var fval = fcp[key][key2].toLowerCase();

                    // Fix #142: search use formated data
                    if (thisColumn && thisColumn.searchFormatter) {
                        value = $.fn.bootstrapTable.utils.calculateObjectValue(
                            that.header,
                            that.header.formatters[$.inArray(key, that.header.fields)],
                            [value, item, i],
                            value
                        );
                    }

                    if (thisColumn.filterStrictSearch) {
                        if (!($.inArray(key, that.header.fields) !== -1 &&
                            (typeof value === 'string' || typeof value === 'number') &&
                            value.toString().toLowerCase() === fval.toString().toLowerCase())) {
                            return false;
                        }
                    } else if (thisColumn.filterStartsWithSearch) {
                        if (!($.inArray(key, that.header.fields) !== -1 &&
                            (typeof value === 'string' || typeof value === 'number') &&
                            (value + '').toLowerCase().indexOf(fval) === 0)) {
                            return false;
                        }
                    } else {
                        if (!($.inArray(key, that.header.fields) !== -1 &&
                            (typeof value === 'string' || typeof value === 'number') &&
                            (value + '').toLowerCase().indexOf(fval) !== -1)) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }) : this.data;
    };

    BootstrapTable.prototype.initColumnSearch = function (filterColumnsDefaults) {
        copyValues(this);

        if (filterColumnsDefaults) {
            this.filterColumnsPartial = filterColumnsDefaults;
            this.updatePagination();

            for (var filter in filterColumnsDefaults) {
                this.trigger('column-search', filter, filterColumnsDefaults[filter]);
            }
        }
    };

    BootstrapTable.prototype.onColumnSearch = function (event) {
        if ($.inArray(event.keyCode, [37, 38, 39, 40]) > -1) {
            return;
        }

        copyValues(this);
        var $target = $(event.currentTarget),
            $th = $target.closest('[data-field]'),
            filterFlag = $target.attr('data-u-flter-flag'),
            text = $.trim($target.val()),
            field = $th.data('field');

        if ($.isEmptyObject(this.filterColumnsPartial)) {
            this.filterColumnsPartial = {};
        }
        if (text) {
            //创建对象
            var obj = {};
            obj[filterFlag] = text;
            //之前的参数清单
            var fcp = this.filterColumnsPartial[field] || {};
            this.filterColumnsPartial[field] = $.extend(true, {}, fcp, obj);
        } else {
            //删除空对象
            delete this.filterColumnsPartial[field][filterFlag];
            if ($.isEmptyObject(this.filterColumnsPartial[field])) {
                delete this.filterColumnsPartial[field];
            }
        }

        // if the searchText is the same as the previously selected column value,
        // bootstrapTable will not try searching again (even though the selected column
        // may be different from the previous search).  As a work around
        // we're manually appending some text to bootrap's searchText field
        // to guarantee that it will perform a search again when we call this.onSearch(event)
        this.searchText += "randomText";

        this.options.pageNumber = 1;
        this.onSearch(event);
        this.trigger('column-search', field, text);
    };

    BootstrapTable.prototype.clearFilterControl = function () {
        if (this.options.filterControl && this.options.filterShowClear) {
            var that = this,
                cookies = collectBootstrapCookies(),
                header = getCurrentHeader(that),
                table = header.closest('table'),
                controls = header.find(getCurrentSearchControls(that)),
                search = that.$toolbar.find('.search input'),
                timeoutId = 0;

            $.each(that.options.valuesFilterControl, function (i, item) {
                item.value = '';
            });

            // Clear each type of filter if it exists.
            // Requires the body to reload each time a type of filter is found because we never know
            // which ones are going to be present.
            if (controls.length > 0) {
                this.filterColumnsPartial = {};
                $(controls[0]).trigger(controls[0].tagName === 'INPUT' ? 'keyup' : 'change');
            } else {
                return;
            }

            if (search.length > 0) {
                that.resetSearch();
            }

            // use the default sort order if it exists. do nothing if it does not
            if (that.options.sortName !== table.data('sortName') || that.options.sortOrder !== table.data('sortOrder')) {
                var sorter = header.find(sprintf('[data-field="%s"]', $(controls[0]).closest('table').data('sortName')));
                if (sorter.length > 0) {
                    that.onSort(table.data('sortName'), table.data('sortName'));
                    $(sorter).find('.sortable').trigger('click');
                }
            }

            // clear cookies once the filters are clean
            clearTimeout(timeoutId);
            timeoutId = setTimeout(function () {
                if (cookies && cookies.length > 0) {
                    $.each(cookies, function (i, item) {
                        if (that.deleteCookie !== undefined) {
                            that.deleteCookie(item);
                        }
                    });
                }
            }, that.options.searchTimeOut);
        }
    };
})(jQuery);
