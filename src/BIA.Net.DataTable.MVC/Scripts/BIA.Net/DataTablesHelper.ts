﻿module BIA.Net.DataTable {

    var DataTableSettings = [];
    export let cultureDataTable: DataTables.LanguageSettings = {
        emptyTable: "No data available in table",
        info: "Showing _START_ to _END_ of _TOTAL_ entries",
        infoEmpty: "Showing 0 to 0 of 0 entries",
        infoFiltered : "(filtered from _MAX_ total entries)",
        infoPostFix: "",
        thousands: ",",
        lengthMenu: "Show _MENU_ entries",
        loadingRecords: "Loading...",
        processing: "Processing...",
        search: "Search:",
        zeroRecords: "No matching records found",
        paginate: {
            first: "First",
            last: "Last",
            next: "Next",
            previous: "Previous"

        },
        aria: {
            sortAscending: ": activate to sort column ascending",
            sortDescending: ": activate to sort column descending"

        }
    }

    export function InitFullAjax(dataTableId, url_GetListData, columns, displayExportButton, getDataExtend, url_OnClickRow, rowCallback) {

        let dom = 'lfrtip';
        var button = null;

        if (displayExportButton === true) {
            dom = 'Bfrtip';
            button = [{
                text: 'Export',
                action: function (e, dt, node, config) {
                    var params = dt.ajax.params();
                    window.location.href = url_GetListData + '?dataJson=' + JSON.stringify(params).replace(',"search":{"value":"","regex":false}', ''); // replace otherwise url too long for a GET
                }
            }];
        }

        $(window).on('OnBIADialogRefresh', function (e) {
            ReloadAjax(dataTableId);
        });
        var dataTableOption = {
            "serverSide": true,
            "language": cultureDataTable,
            "dom": dom,
            "buttons": button,
            ajax: {
                url: url_GetListData,
                type: 'POST'
                ,
                "data": function (d) {
                    if (getDataExtend != null) {
                        return $.extend({}, d, getDataExtend());
                    }
                }
            },
            rowId: 'Id',
            "createdRow": function (row, data, dataIndex) {

                if (url_OnClickRow != null) {
                    // event after the creation of a line. Allows you to add events
                    $(row).css({ 'cursor': "pointer" });
                    $(row).attr('BIADialogLink', "Type:non-modal;Id:" + data.Id);
                    $(row).attr('onclick', "window.location='" + url_OnClickRow + "/" + data.Id + "'");
                }
            },
            "drawCallback": function (settings) {
                BIA.Net.Dialog.LinkToDialog($(this));
                BIA.Net.Dialog.AddRefreshAction($(this));
            },
            "columns": columns,
            "rowCallback": rowCallback
        };
        $(dataTableId).dataTable(dataTableOption);
    }

    export function InitAjax(dataTableId, url_GetListData, columns, exportButtons, formId, url_OnClickRow, rowCallback) {

        let dom = 'lfrtip';

        if (exportButtons != null) {
            dom = 'Bfrtip';
        }

        $(window).on('OnBIADialogRefresh', function (e) {
            ReloadAjax(dataTableId);
        });

        var dataTableOption = {
            "language": cultureDataTable,
            "dom": dom,
            "buttons": exportButtons,
            ajax: {
                url: url_GetListData,
                "data": function (d) {
                    // allows to push the filter data to the controller
                    if (formId != null) {
                        return $(formId).serialize();
                    }
                },
                "dataSrc": 'data'
            },
            rowId: 'Id',
            "createdRow": function (row, data, dataIndex) {

                if (url_OnClickRow != null) {
                    // event after the creation of a line. Allows you to add events
                    $(row).css({ 'cursor': "pointer" });
                    $(row).attr('BIADialogLink', "Type:non-modal;Id:" + data.Id);
                    $(row).attr('onclick', "window.location='" + url_OnClickRow + "/" + data.Id + "'");
                }
            },
            "drawCallback": function (settings) {
                BIA.Net.Dialog.LinkToDialog($(this));
                BIA.Net.Dialog.AddRefreshAction($(this));
            },
            "columns": columns,
            "rowCallback": rowCallback
        };
        $(dataTableId).dataTable(dataTableOption);
    }

    export function ReloadAjax(dataTableId) {
        $(dataTableId).DataTable().ajax.reload(null, false);
    }

    export function InitWithFullCustomisableOptions(dataTableId, dataTableOptions) {
        dataTableOptions.language = DataTable.cultureDataTable;
        $(dataTableId).DataTable(dataTableOptions);
        $(window).on('OnBIADialogRefreshed', function () {
            $(dataTableId).DataTable(dataTableOptions);
        });
    }

    // ExportButtons can be : ['copy', 'csv', 'excel', 'print'] or null
    export function InitStandard(dataTableId, exportButtons) {
        if (exportButtons == null) {
            $(document).ready(function () {
                let dataTableOption : DataTables.Settings = {
                    "language": cultureDataTable
                };
                $(dataTableId).DataTable(dataTableOption);
            });
        }
        else {
            $(document).ready(function () {
                var dataTableOption = {
                    "language": cultureDataTable,
                    "dom": 'Bfrtip',
                    "buttons": exportButtons
                }
                $(dataTableId).DataTable(dataTableOption);
            });
        }

        DataTableSettings[dataTableId] = null;
        $(window).on('OnBIADialogRefreshing', function (e) {
            var dataTable = e.BIANetDialogData.targetElem.find(dataTableId);

            if (dataTable.length == 1) {
                var tableSettings = dataTable.dataTable().fnSettings();
                var pageLength = tableSettings._iDisplayLength;
                var displayStart = tableSettings._iDisplayStart;
                var search = tableSettings.oPreviousSearch.sSearch;
                var order = tableSettings.aaSorting;
                DataTableSettings[dataTableId] = { pageLength: pageLength, displayStart: displayStart, search: search, order: order };
            }
        });
        if (exportButtons == null) {
            $(window).on('OnBIADialogRefreshed', function (e) {
                var settings = DataTableSettings[dataTableId];
                e.BIANetDialogData.targetElem.find(dataTableId).each(function () {
                    $(this).DataTable({
                        "language": cultureDataTable,
                        "pageLength": settings.pageLength,
                        "displayStart": settings.displayStart,
                        "search": { "search": settings.search },
                        "order": settings.order,
                        retrieve: true
                    })
                });
            });
        }
        else {
            $(window).on('OnBIADialogRefreshed', function (e) {
                var settings = DataTableSettings[dataTableId];
                e.BIANetDialogData.targetElem.find(dataTableId).each(function () {
                    var dataTableOption = {
                        "language": cultureDataTable,
                        "dom": 'Bfrtip',
                        "buttons": exportButtons,
                        "pageLength": settings.pageLength,
                        "displayStart": settings.displayStart,
                        "search": { "search": settings.search },
                        "order": settings.order,
                        retrieve: true
                    };
                    $(this).DataTable(dataTableOption)
                });
            });
        }
    }
}