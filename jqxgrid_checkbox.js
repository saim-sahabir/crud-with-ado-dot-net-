$(document).ready(function () {
    
    var data = [
      { name: 'Ian', dateString: '20160101' },
      { name: 'Cheryl', dateString: '20160814' },
      { name: 'Mark', dateString: '20161111' },
      { name: 'Petra', dateString: '20161024' }
    ];
    
    var source = {
      localdata: data,
      datafields: [{
        name: 'firstname',
        map: 'name',
        type: 'string'
      }, {
        name: 'date',
        map: 'dateString',
        type: 'date',
        format: 'yyyyMMdd'
      }],
      datatype: "array"
    };
    
    var adapter = new $.jqx.dataAdapter(source);
    var dateStringFormat = 'dd-MM-yyyy';
    
    var isDisabled = false;
    var isSelectedDisabledRow = false;
    var disableRow = 1;
    var cellbeginedit = function (row, datafield, columntype, value) {
      if (isDisabled) {
        if (row == disableRow) return false;
      }
    };
    var cellsrenderer = function (row, column, value, defaultHtml) {
        if (isDisabled) {
            if (row == disableRow) {
                var element = $(defaultHtml);
                element.css('color', '#999');
                return element[0].outerHTML;
            }
        }
    
      return defaultHtml;
    };
    
    $("#jqxgrid").jqxGrid({
      width: 400,
      autoheight: true,
      theme: 'energyblue',
      source: adapter,
      selectionmode: 'checkbox',
      filterable: true,
      columns: [{
        text: 'First Name',
        datafield: 'firstname',
        columngroup: 'Name',
        width: '30%', 
        cellbeginedit: cellbeginedit, 
        cellsrenderer: cellsrenderer
      }, {
        text: 'Order Date',
        datafield: 'date',
        cellsformat: 'dd-MM-yyyy',
        align: 'right',
        cellsalign: 'center', 
        cellbeginedit: cellbeginedit, 
        cellsrenderer: cellsrenderer
      }]
    });
    
    var isRowselectEvent = false;
    $('#jqxgrid').on('rowselect', function (event) {
        // event arguments.
        var args = event.args;
        // row's bound index.
        var rowBoundIndex = args.rowindex;
        // row's data. The row's data object or null(when all rows are being selected or unselected with a single action). If you have a datafield called "firstName", to access the row's firstName, use var firstName = rowData.firstName;
        var rowData = args.row;
        isRowselectEvent = false;
        if (isDisabled) {
        	if (rowBoundIndex == disableRow) {
              	if (!isSelectedDisabledRow && !isRowunselectEvent) {
                	isRowselectEvent = true;
                    $('#jqxgrid').jqxGrid('unselectrow', disableRow);
                }  
            }  	
        }
    });
    var isRowunselectEvent = false;
    $('#jqxgrid').on('rowunselect', function (event) {
        // event arguments.
        var args = event.args;
        // row's bound index.
        var rowBoundIndex = args.rowindex;
        var rowData = args.row;
        isRowunselectEvent = false;
        if (isDisabled) {
        	if (rowBoundIndex == disableRow) {
          	    if (isSelectedDisabledRow && !isRowselectEvent) {
                	isRowunselectEvent = true;
                    $('#jqxgrid').jqxGrid('selectrow', disableRow);
                }        	
            }  	
        }
    });
    
    $("#click").jqxButton({ theme: 'energyblue' });
    $("#click").click(function () {
      isDisabled = true;
      var rowindexes = $('#jqxgrid').jqxGrid('getselectedrowindexes');
      isSelectedDisabledRow = rowindexes.indexOf(disableRow) != -1;
      $('#jqxgrid').jqxGrid('updatebounddata', 'cells');
    });
    
});

<HTML>
	<HEAD>
	</HEAD>
	<BODY>
		<div id="jqxgrid"></div>
        <button id="click">Disable Second Row</button>
	</BODY>
</HTML>
