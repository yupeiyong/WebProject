var containers = document.getElementsByClassName('container');
if (containers.length > 0) {
    var container = containers[0];
    var id = container.attributes['id'];
    var selector = '.nav li.' + id.nodeValue;
    var li = container.querySelector(selector);
    if (li) {
        var liClass = li.attributes['class'];
        if (liClass) {
            var classValue = liClass.nodeValue;
            if (classValue.length === 0) {
                classValue = "on";
            } else {
                classValue += " on";
            }
            liClass.nodeValue = classValue;
        } else {
            li.attributes['class'] = "on";
        }
    }
}