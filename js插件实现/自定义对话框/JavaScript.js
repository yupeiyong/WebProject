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
    var dialog = function () {
        this.init()
    };

    dialog.prototype = {
        init: function () {

        },
        show: function () {
            alert('show');
        }
    };
    _global = (function () { return this || (0, eval)('this'); }());
    !('dialog' in _global) && (_global.dialog=dialog);
}());