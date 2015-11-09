
// 用于easyui datagrid 得到后台传送的日期格式化方法
function dateTimeFormatter(value, row, index) {
    if (value == null)
        return '';

    var obj = eval('(' + " new " + value.substr(1, value.length - 2) + ')');
    return moment(obj).format('YYYY-MM-DD HH:mm:ss')
}


// 弹出浮层
var layerWindowName = 'floatingLayerWindow';

// 关闭当前子窗体, 并刷新父窗体
function closeWindow() {
    parent.$('#' + layerWindowName).window('close');
    parent.location.reload();
}

// 页面初始化时加入新的用于显示浮动页面的层。
$(function () {
    $('body').append('<div id="' + layerWindowName + '" closed="true"></div>')
})

// 加入需显示的内容，并打开此层
function showWindow(title, href, width, height, modal, minimizable, maximizable) {
    $('#'+layerWindowName).window(
    {
        title: title,
        //href: href,
        width: width,
        height: height,
        iconCls: 'icon-edit',

        content: '<iframe scrolling="yes" frameborder="0"  src="'
                + href
                + '" style="width:98%;height:98%;" ></iframe>',

        modal: modal === undefined ? true : modal,
        minimizable: minimizable === undefined ? false
                : minimizable,
        maximizable: maximizable === undefined ? false
                : maximizable,
        closed: true,
        loadingMessage: '正在加载数据，请稍等片刻......'
    });
    $('#' + layerWindowName).window('open');
}