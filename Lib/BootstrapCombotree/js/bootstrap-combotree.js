/**
 * Created by CherryDream on 2016/8/22.
 */

var Text = new Array() ;//按钮上的文本
var Value = new Array();//值
var combotree = "combotree";
(function ($, window, document, undefined) {
$.fn[combotree] = function (options, args) {
    var result;

    this.each(function () {
        var _this = $.data(this, combotree);
        if (typeof options === 'string') {
            if (!_this) {
              window.console.error("参数错误");
            }
            else if (!$.isFunction(_this[options]) || options.charAt(0) === '_') {
                window.console.error('没有这个参数 : ' + options);
            }
            else {
                if (!(args instanceof Array)) {
                    args = [ args ];
                }
                result = _this[options].apply(_this, args);
            }
        }
        else if (typeof options === 'boolean') {
            result = _this;
        }
        else {
            $.data(this, combotree, new comboTree(this, $.extend(true, {}, options)));
        }
    });

    return result || this;
};

//todo 现阶段未完成方法setValue getValue的编写，已经完成表单提交操作
var comboTree = function (element, options) {
    //写html标签
    if(options.width == undefined)
    {
        element.innerHTML = '<div class="btn-group">'
            + '<button type="button" class="btn btn-default dropdown-toggle"   data-toggle="dropdown" title=' + options.defaultLable +'>'
            + options.defaultLable + '<span class="caret"></span>'
            + '</button>'
            + '<input type="hidden" name="' + options.name + '"/> '
            + '<div class="dropdown-menu" style="width: 400%"></div>'
            + '</div>';
    }
    else
    {
        element.innerHTML = '<div class="btn-group">'
            + '<button type="button" class="btn btn-default dropdown-toggle"  data-toggle="dropdown" title=' + options.defaultLable +'>'
            + options.defaultLable + '<span class="caret"></span>'
            + '</button>'
            + '<input type="hidden" name="' + options.name + '"/> '
            + '<div class="dropdown-menu" style="width: ' + options.width+ 'px;"></div>'
            + '</div>';
    }
    var $Tree = $(element).find(".dropdown-menu");//Tree对象
    var $Button = $(element).find("button");//button对象
    var $hidden = $(element).find("input[type='hidden']");//隐藏域
    //渲染bootstrap-treeview
    $Tree.treeview({
        data : options.data,
        showIcon: options.showIcon,
        showCheckbox : options.showCheckbox,
        onNodeChecked: function(event, node) {
            getCheckValue(node, $Button, $hidden, options.defaultLable, $Tree);
            checkparent(node, $Tree);
            if(Text.length <= options.maxItemsDisplay)
            {
                $Button[0].innerHTML = Text + '<span class="caret"></span>';
                $Button.attr("title", Text);
            }
           else
            {
                $Button[0].innerHTML = Text.length  + '项被选中  <span class="caret"></span>';
                $Button.attr("title", Text.length  + '项被选中');
            }
            $hidden.val(Value);
            if(options.onCheck != undefined)
            {
                options.onCheck(node);
            }
        },
        onNodeUnchecked: function (event, node) {
            setUnCheck(node, $Button, $hidden, options.defaultLable, $Tree);
            unCheckparent(node, $Tree);
            if (Text.length == 0) {
                $Button[0].innerHTML = options.defaultLable + '<span class="caret"></span>';
                $Button.attr("title", options.defaultLable);
            }
            else {
                if (Text.length <= options.maxItemsDisplay) {
                    $Button[0].innerHTML = Text + '<span class="caret"></span>';
                    $Button.attr("title", Text);
                }
                else {
                    $Button[0].innerHTML = Text.length + '项被选中 <span class="caret"></span>';
                    $Button.attr("title",  Text.length + '项被选中');
                }
            }
            if(options.onAfterUnCheck != null)
            {
                options.onAfterUnCheck(node);
            }
        },
        onNodeCollapsed : function (event, node) {

        },
        onNodeExpanded : function (event, node) {
        },
        onNodeSelected: function(event, node) {
        },
        onNodeUnselected: function (event, node) {
        }
    });
    // todo 此时返回false可以阻止下拉框被隐藏
    $(element).on('hide.bs.dropdown', function (e) {
    });
}


})(jQuery, window, document);

/**
 * 选定节点及其子节点,并将值赋予值域
 * @param node 节点
 * @param $Button 组件的button按钮，修改时无需修改此处
 * @param $hidden 组件的值域，修改时无需修改此处
 * @param defaultText 组件的按钮默认文字，修改时无需修改此处
 */
function getCheckValue(node, $Button, $hidden, defaultText, $Tree) {
    $Tree.treeview('checkNode', [ $Tree.treeview('searchById', [ node.id, { ignoreCase: true, exactMatch: false } ]), { silent: true}]);
    if($Button[0].innerText == defaultText)
    {
        $Button[0].innerText = '';
    }
    if(node == undefined)
    {
        return;
    }
    else
    {

        if(node.nodes == undefined)
        {
            if($.inArray(node.id, Value) < 0)
            {
                Value.push(node.id);
                Text.push(node.text);
            }
        }
        else
        {

            for(var i = 0; i < node.nodes.length; i++)
            {
                getCheckValue(node.nodes[i], $Button, $hidden, defaultText, $Tree);
            }
        }
    }
}

function check() {

}
/**
 * 取消选定节点及其子节点,并将值从值域中取出
 * @param node 节点
 * @param $Button 组件的button按钮，修改时无需修改此处
 * @param $hidden 组件的值域，修改时无需修改此处
 * @param defaultText 组件的按钮默认文字，修改时无需修改此处
 */
function setUnCheck(node, $Button, $hidden, defaultText, $Tree) {
    $Tree.treeview('uncheckNode', [ $Tree.treeview('searchById', [ node.id, { ignoreCase: true, exactMatch: false } ]), { silent: true}]);
    if(node == undefined)
    {
        return;
    }
    else
    {
        if(node.nodes == undefined)
        {
            for(var i = 0; i < Value.length; i++)
            {
                if (Text[i] == node.text) {
                    Text.splice(i, 1);
                }
                if (Value[i] == node.id) {
                    Value.splice(i, 1);
                }
            }
            $hidden.val(Value);
        }
        else
        {
            for(var i = 0; i < node.nodes.length; i++)
            {
                setUnCheck(node.nodes[i], $Button, $hidden, defaultText, $Tree);
            }
        }
    }
}

/**
 * 选择父节点
 * @param node
 * @param $Tree
 */
function checkparent (node, $Tree) {
    if(node.parentId == undefined)
    {
        return;
    }
    var pNode = $Tree.treeview('getNode', [ node.parentId, { ignoreCase: true, exactMatch: false } ]);
    $Tree.treeview('checkNode', [ pNode, { silent: true}]);
    checkparent(pNode, $Tree);
}

/**
 * 取消选择父节点
 * @param node
 * @param $Tree
 */
function unCheckparent(node, $Tree) {
    if (node.parentId == undefined) {
        return;
    }
    var pNode = $Tree.treeview('getNode', [node.parentId, {ignoreCase: true, exactMatch: false}]);
    var flag = true;
    for(var i = 0; i < pNode.nodes.length; i++)
    {
        if(pNode.nodes[i].state.checked)
        {
            flag = false;
            break;
        }

    }
    if(flag)
    {
        $Tree.treeview('uncheckNode', [ pNode, { silent: true}]);
        unCheckparent(pNode, $Tree);
    }
    else
    {
        return;
    }
}