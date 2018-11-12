(function () {
    "use strict"
    var _global;

    var extend = function (original, newObj, isOverride) {
        for (var key in newObj) {
            if(newObj.hasOwnProperty(key) && (!newObj.hasOwnProperty(key)||isOverride )){
                original[key]=newObj[key];
            }
        }
        return original;
    }
    var dialog = function (options) {
        this._init(options)
    };
    function templateEngine(html, data) {
        var re = /<%([^%>]+)?%>/g,
            reExp = /(^( )?(if|for|else|switch|case|break|{|}))(.*)?/g,
            code = 'var r=[];\n',
            cursor = 0;
        var match;
        var add = function (line, js) {
            js ? (code += line.match(reExp) ? line + '\n' : 'r.push(' + line + ');\n') :
                (code += line != '' ? 'r.push("' + line.replace(/"/g, '\\"') + '");\n' : '');
            return add;
        }
        while (match = re.exec(html)) {
            add(html.slice(cursor, match.index))(match[1], true);
            cursor = match.index + match[0].length;
        }
        add(html.substr(cursor, html.length - cursor));
        code += 'return r.join("");';
        return new Function(code.replace(/[\r\t\n]/g, '')).apply(data);
    }

    // 通过class查找dom
    if (!('getElementsByClass' in HTMLElement)) {
        HTMLElement.prototype.getElementsByClass = function (n) {
            var el = [],
                _el = this.getElementsByTagName('*');
            for (var i = 0; i < _el.length; i++) {
                if (!!_el[i].className && (typeof _el[i].className == 'string') && _el[i].className.indexOf(n) > -1) {
                    el[el.length] = _el[i];
                }
            }
            return el;
        };
        ((typeof HTMLDocument !== 'undefined') ? HTMLDocument : Document).prototype.getElementsByClass = HTMLElement.prototype.getElementsByClass;
    }

    dialog.prototype = {
        _init: function (options) {
            // 默认参数
            var def = {
                ok: true,
                ok_txt: '确定',
                cancel: false,
                cancel_txt: '取消',
                confirm: function () { },
                close: function () { },
                content: '',
                templateId: null
            };
            this.options = extend(def, options, true);
            this.dialogDom = this._templateToDom(this.options.templateId);
            this.hasDom = false;
            this.listens = [];
            this.handlers = {};
        },
        _templateToDom: function (templateId) {
            var templateInnerHtml = document.getElementById(templateId).innerHTML.trim();
            var str = templateEngine(templateInnerHtml, this.options);
            var div = document.createElement('div');
            div.innerHTML = str;
            return div.childNodes[0];
        },
        show: function (callback) {
            var _this = this;
            if (_this.hasDom) return _this;

            if (_this.listens.indexOf('show') >= 0) {
                _this.handlerEvent({ type: 'show', target: _this.dialogDom });
            }

            document.body.appendChild(this.dialogDom);
            _this.hasDom = true;

            _this.dialogDom.getElementsByClass('dialog-close')[0].onclick =function () {
                _this.hide();
                if (_this.listens.indexOf('close') >= 0) {
                    _this.handlerEvent({ type: 'close', target: _this.dialogDom })
                }
            };

            _this.dialogDom.getElementsByClass('btn-ok')[0].onclick = function () {
                _this.hide();
                if (_this.listens.indexOf('confirm') >= 0) {
                    _this.handlerEvent({ type: 'confirm', target: _this.dialogDom })
                }
            };

            if (_this.options.cancel) {
                _this.dialogDom.getElementsByClass('btn-cancel')[0].onclick = function () {
                    _this.hide();
                    if (_this.listens.indexOf('cancel') >= 0) {
                        _this.handlerEvent({ type: 'cancel', target: _this.dialogDom })
                    }
                };
            }

            callback && callback();
            if (_this.listens.indexOf('shown') >= 0) {
                _this.handlerEvent({ type: 'shown', target: _this.dialogDom })
            }
            return _this;
        },
        hide: function (callback) {
            if (this.listens.indexOf('hide') > -1) {
                if (!this.handlerEvent({ type: 'hide', target: this.dialogDom })) return;
            }
            document.body.removeChild(this.dialogDom);
            this.hasDom = false;
            callback && callback();
            if (this.listens.indexOf('hidden') > -1) {
                this.handlerEvent({ type: 'hidden', target: this.dialogDom })
            }
            return this;
        },
        css: function (styleobj) {
            for (var prop in styleobj) {
                var attr = prop.replace(/[A-Z]/g, function (word) {
                    return '-' + word.toLowerCase();
                });
                this.dom.style[attr] = styleobj[prop];
            }
            return this;
        },
        width: function (val) {
            this.dialogDom.style.width = val + 'px';
            return this;
        },
        height: function (val) {
            this.dialogDom.style.height = val + 'px';
            return this;
        },
        on: function (type, handler) {
            if (typeof this.handlers[type] === 'undefined') {
                this.handlers[type] = [];
            }
            this.listens.push(type);
            this.handlers[type].push(handler);
            return this;
        },
        off: function (type, handler) {
            if (this.handlers[type] instanceof Array) {
                var handlers = this.handlers[type];
                for (var i = 0, len = handlers.length; i < len; i++) {
                    if (handlers[i] === handler) {
                        break;
                    }
                }
                this.listens.splice(i, 1);
                handlers.splice(i, 1);
                return this;
            }
        },
        handlerEvent: function (event) {
            if (!event.target) {
                event.target = this;
            }
            if (this.handlers[event.type] instanceof Array) {
                var handlers = this.handlers[event.type];
                for (var i = 0, len = handlers.length; i < len; i++) {
                    handlers[i](event);
                    return true;
                }
            }
            return false;
        }
    };
    _global = (function () { return this || (0, eval)('this'); }());
    !('dialog' in _global) && (_global.dialog=dialog);
}());