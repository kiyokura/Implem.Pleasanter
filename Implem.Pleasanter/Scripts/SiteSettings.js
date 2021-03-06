﻿$p.uploadSiteImage = function ($control) {
    var data = new FormData();
    data.append('SiteImage', $('#SiteImage').prop('files')[0]);
    $p.upload(
        $('.main-form').attr('action').replace('_action_', $control.attr('data-action')),
        $control.attr('data-method'),
        data);
}

$p.openSiteSettingsDialog = function ($control, selector) {
    var error = $p.syncSend($control);
    if (error === 0) {
        $(selector).dialog({
            modal: true,
            width: '90%',
            height: 'auto',
            appendTo: '#Editor'
        });
    }
}

$p.openGridColumnDialog = function ($control) {
    $p.data.GridColumnForm = {};
    $p.openSiteSettingsDialog($control, '#GridColumnDialog');
}

$p.setGridColumn = function ($control) {
    $p.setData($('#UseGridDesign'));
    $p.setData($('#GridDesign'));
    $p.send($control);
}

$p.openFilterColumnDialog = function ($control) {
    $p.data.FilterColumnForm = {};
    $p.openSiteSettingsDialog($control, '#FilterColumnDialog');
}

$p.openEditorColumnDialog = function ($control) {
    $p.data.EditorColumnForm = {};
    $p.openSiteSettingsDialog($control, '#EditorColumnDialog');
}

$p.openSummaryDialog = function ($control) {
    $p.data.SummaryForm = {};
    $p.openSiteSettingsDialog($control, '#SummaryDialog');
}

$p.setSummary = function ($control) {
    $p.setData($('#EditSummary'), $p.getData($control));
    $p.send($control);
}

$p.openFormulaDialog = function ($control) {
    $p.data.FormulaForm = {};
    $p.openSiteSettingsDialog($control, '#FormulaDialog');
}

$p.openViewDialog = function ($control) {
    $p.data.ViewForm = {};
    $p.openSiteSettingsDialog($control, '#ViewDialog');
}

$p.openNotificationDialog = function ($control) {
    $p.data.NotificationForm = {};
    $p.openSiteSettingsDialog($control, '#NotificationDialog');
}

$p.setNotification = function ($control) {
    $p.setData($('#EditNotification'), $p.getData($control));
    $p.send($control);
}

$p.setAggregationDetails = function ($control) {
    var data = $p.getData($control);
    data.AggregationType = $('#AggregationType').val();
    data.AggregationTarget = $('#AggregationTarget').val();
    $('.ui-dialog-content').dialog('close');
    $p.send($control);
}

$p.addSummary = function ($control) {
    $p.setData($('#SummarySiteId'));
    $p.setData($('#SummaryDestinationColumn'));
    $p.setData($('#SummaryLinkColumn'));
    $p.setData($('#SummaryType'));
    $p.setData($('#SummarySourceColumn'));
    $p.send($control);
}