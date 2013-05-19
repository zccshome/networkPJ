function sendback() {
    var digits = new Array();

    var length = document.getElementById("hf1").value;
    var i = 0;
    var count = 0;
    for (; i < length; i++) {
        var object = document.getElementById("cbl_" + i);
        if (object.checked == true)
            digits[count++] = object.value;
    }

    var array = array2string(digits);
    window.opener.document.getElementById("MainContent_tb" + document.getElementById("hf2").value).value = getNames(array);
    window.opener.document.getElementById("MainContent_hf" + document.getElementById("hf2").value).value = getIds(array);
    //document.getElementById("const").value = array2string(digits);
    window.close();

    return false;
}

function array2string(array) {
    var length = array.length;
    var text = "";
    var i = 0;
    for (; i < length; i++) {
        if (i == length - 1) {
            text += array[i];
        }
        else {
            text += (array[i] + ",");
        }
    }
    return text;
}

function getNames(str) {
    if (str == "")
        return "";
    var array = new Array();
    var count = 0;
    var content = str.split(",");//content数组包含的是请求的编号，比如0,2;0,1;1,2
    var raw = window.opener.document.getElementById("MainContent_gnname").value.split(",");//raw数组包含的是实际的名字
    for (var a = 0; a < content.length; a++)
        array[a] = raw[content[a]];

    return array;
}

function getIds(str) {
    if (str == "")
        return "";
    var array = new Array();
    var count = 0;
    var content = str.split(",");//content数组包含的是请求的编号，比如0,2;0,1;1,2
    var raw = window.opener.document.getElementById("MainContent_gnid").value.split(",");//raw数组包含的是实际的名字
    for (var a = 0; a < content.length; a++)
        array[a] = raw[content[a]];

    return array;
}
