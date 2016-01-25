var mainPage = {
    onMenuClick: function (id, name, url) {
        if (id == -1)
            return;

        var tabId = "tab" + id;
        var maintabs = $("#mainTabs");
        var tab = maintabs.tabs("getTabById", tabId);
        if (!tab) {
            maintabs.tabs('add', {
                id: tabId,
                title: name,
                href: url,
                selected: true,
                closable: true
            });
        }

        maintabs.tabs('select', name);
    },

    // 调用 'refresh' 方法更新选项卡面板的内容
    refreshTab: function () {
        var tab = $('#mainTabs').tabs('getSelected');  // 获取选择的面板
        if (tab) {
            //console.log(tab);
            //alert(tab.panel('options').id);
            //tab.panel('clear');
            tab.panel('refresh');
        }
          
    }
};