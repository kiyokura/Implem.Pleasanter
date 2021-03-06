﻿$p.setDropDownSearch = function () {
    var $control = $('#' + $('#DropDownSearchTarget').val());
    if ($control.attr('multiple') === 'multiple') {
        $control.multiselect('refresh');
    }
    $p.setData($control);
    if ($control.hasClass('auto-postback')) {
        $p.send($control);
    }
}

$p.openDropDownSearchDialog = function ($control) {
    var id = $control.attr('id');
    var $text = $('#DropDownSearchText');
    var $target = $('#DropDownSearchTarget');
    $('#DropDownSearchResults').empty();
    $target.val(id);
    $text.val('');
    $('#DropDownSearchMultiple').val($control.attr('multiple') === 'multiple');
    $p.send($text);
    $($('#DropDownSearchDialog')).dialog({
        title: $('label[for="' + id + '"]').text(),
        modal: true,
        width: '630px',
        close: function () {
            $('#' + $target.val()).prop("disabled", false);
        }
    });
    $control.prop("disabled", true);
    $text.focus();
}