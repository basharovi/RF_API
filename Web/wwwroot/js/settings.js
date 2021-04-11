var yearArr = [{ text: "2019", id: "2019" }, { text: "2020", id: "2020" }, { text: "2021 ", id: "2021" }, { text: "2022", id: "2022" }, { text: "2023", id: "2023" }, { text: "2024", id: "2024" }, { text: "2025", id: "2025" }, { text: "2026", id: "2026" }, { text: "2027", id: "2027" }, { text: "2028", id: "2028" }, { text: "2029", id: "2029" }, { text: "2030", id: "2030" }];
var monthArr = [
    { text: "January", id: "1" },
    { text: "February", id: "2" },
    { text: "March", id: "3" },
    { text: "April", id: "4" },
    { text: "May", id: "5" },
    { text: "June", id: "6" },
    { text: "July", id: "7" },
    { text: "August", id: "8" },
    { text: "September", id: "9" },
    { text: "October", id: "10" },
    { text: "November", id: "11" },
    { text: "December", id: "12" }];
var quarterArr = [
    { text: "Quarter-01", id: "1" },
    { text: "Quarter-02", id: "2" },
    { text: "Quarter-03", id: "3" },
    { text: "Quarter-04", id: "4" }];

var gradeArr = [

    { text: "Class Four", id: "4" },
    { text: "Class Five", id: "5" }
];



var vurl; var vtableBody; var vformId; var visEdit; var veditUrl; var visDelete; var vdeleteUrl; var vprimaryKey;

function LoadList(url, tableBody, formId, isEdit, editUrl, isDelete, deleteUrl, primaryKey, callbackFn) {
    this.vurl = url; this.vtableBody = tableBody; this.vformId = formId; this.visEdit = isEdit; this.veditUrl = editUrl; this.visDelete = isDelete; this.vdeleteUrl = deleteUrl; this.vprimaryKey = primaryKey;
    rf.api.actionCall(url, 'GET', {}, function (data, status) {
        if (status) {

            var html = '';
            for (var i = 0; i < data.Data.length; i++) {
                html += tableRowHtmlContent(data.Data[i]);
            }
            var tableId = $('#' + tableBody)[0].parentNode.id;
            var colCount = columnCount('#' + tableId);
            if (data.Data.length == 0) {
                html += '<tr class="no-data-found"><td class="no-data-found" colspan="' + colCount + '"><div id="roleList" class="rf-steper-user" style="position: relative;"><div class="rf-stepper-noRecords animate-zoomIn"><img src="/Images/not-found.png"><div>No data added</div></div></div></td></tr>'
            }
            $('#' + tableBody).html(html);
            $('#' + tableId).wrap("<div id='tableWrapper' style='display:block;'></div>");
            $('#tableWrapper').slimScroll({
                height: '65vh',
                color: 'rgba(0, 0, 0, 0.3)',
                size: '5px'
            });
        }
        else
            rf.ui.message.error(data.Data);
    });
}

function columnCount(selector) {
    var colCount = 0;
    $(selector + ' tr:nth-child(1) th').each(function () {
        if ($(this).attr('colspan')) {
            colCount += +$(this).attr('colspan');
        } else {
            colCount++;
        }
    });
    return colCount;
};

function loadEditForm(id) {
    rf.api.actionCall('/View/Settings/' + this.vformId + 'Form?' + this.vprimaryKey + '=' + id, 'POST', {}, function (data, status) {
        rf.ui.setHtml('#' + this.vformId + 'FormDiv', data);
        switch (this.vformId) {
            case "CourseInfo": DropdownForCourse(true); break;
            case "UnitInfo": DropdownForUnit(true); break;
            case "VillageInfo": DropdownForVillage(true); break;
            case "SchoolInfo": DropdownForSchool(true); break;
        }
    });
}

function editListItem(e, id) {
    e.preventDefault();
    loadEditForm(id);
    $(document).scrollTop(0);
}
function deleteListItem(e, id) {
    e.preventDefault();
    rf.ui.message.showConfirm('Do you want to delete', function (isConfirm) {
        if (isConfirm) {
            rf.api.actionCall(this.vdeleteUrl + id, 'GET', {}, function (data, status) {
                if (status) {
                    rf.ui.message.success(data.Data);
                    LoadList(this.vurl, this.vtableBody, this.vformId, this.visEdit, this.veditUrl, this.visDelete, this.vdeleteUrl, this.vprimaryKey);
                }
                else
                    rf.ui.message.error(data.Data);
            });
        }
    });
}

function tableRowHtmlContent(json) {

    var mkeys = Object.keys(json);
    var vhtml = '<tr>';
    mkeys.forEach(function (item) {
        if (item != this.vprimaryKey)
            vhtml += '<td>' + (json[item] || "") + '</td>';
    });

    if (this.visEdit == true && this.visDelete == true) {
        vhtml += ' <td style="text-align:center"><span class="hi-icon-effect-3 hi-icon-effect-3b" style="display:inline;"><i style="margin-right: 4px;" class="hi-icon fa fa-pencil icon-fa "><a href="#" onclick="editListItem(event,' + json[this.vprimaryKey] + ')"></a></i><i class="hi-icon fa fa-trash icon-fa "><a href="#" onclick="deleteListItem(event,' + json[this.vprimaryKey] + ')"></a></i></span></td></tr>'
    }
    else if (this.visEdit == true && this.visDelete == false) {
        vhtml += '<td style="text-align:center"><span class="hi-icon-effect-3 hi-icon-effect-3b" style="display:inline;"><i style="margin-right: 4px;" class="hi-icon fa fa-pencil icon-fa "><a href="#" onclick="editListItem(event,' + json[this.vprimaryKey] + ')"></a></i></span></td></tr>'
    }
    else if (this.visEdit == false && this.visDelete == true) {
        vhtml += '<td style="text-align:center"><span class="hi-icon-effect-3 hi-icon-effect-3b" style="display:inline;"><i class="hi-icon fa fa-trash icon-fa "><a href="#" onclick="deleteListItem(event,' + json[this.vprimaryKey] + ')"></a></i></span></td></tr>'
    }
    return vhtml;
}

function Save(calBackFn) {
    rf.db.save('#' + this.vformId, function (data, status) {
        if (status) {
            rf.ui.message.success("Data saved successfully");
            loadEditForm(0);
            LoadList(this.vurl, this.vtableBody, this.vformId, this.visEdit, this.veditUrl, this.visDelete, this.vdeleteUrl, this.vprimaryKey);
        }
        else {
            rf.ui.message.error(data.Data);
        }
        if (typeof (calBackFn) != 'undefined') {
            calBackFn(data, status);
        }
    });
}


function DropdownForCourse(updateMode) {
    rf.ui.remoteBind('#GradeId', '/control/ctddl/GradeInfo/GradeName/GradeId');
}
function DropdownForUnit(updateMode) {
    rf.ui.remoteBind('#GradeId', '/control/ctddl/GradeInfo/GradeName/GradeId', {}, {}, undefined, 'change', function () {
        rf.ui.remoteBind('#CourseId', '/control/ctddlcasede/CourseInfo/CourseName/CourseId/' + JSON.stringify({ "GradeId": $("#GradeId").val() }));
    }, updateMode, true);
}
function DropdownForVillage(updateMode) {
    rf.ui.remoteBind('#DivisionCode', '/control/ctddl/AllDivision/DivisionName/DivisionCode', {}, {}, undefined, 'change', function () {
        rf.ui.remoteBind('#DistrictCode', '/control/ctddlcasede/AllDistrict/DistrictName/DistrictCode/' + JSON.stringify({ "DivisionCode": "" + $("#DivisionCode").val() }), {}, {}, undefined, 'change', function () {
            rf.ui.remoteBind('#UpazilaCode', '/control/ctddlcasede/AllUpazila/UpazilaName/UpazilaCode/' + JSON.stringify({ "DistrictCode": "" + $("#DistrictCode").val() }), {}, {}, undefined, 'change', function () {
                rf.ui.remoteBind('#UnionCode', '/control/ctddlcasede/AllUnion/UnionName/UnionCode/' + JSON.stringify({ "DistrictCode": "" + $("#DistrictCode").val(), "UpazilaCode": "" + $("#UpazilaCode").val() }), {}, {}, undefined, 'change', function () {
                }, updateMode, true);
            }, updateMode, true);
        }, updateMode, true);
    }, updateMode, true);
}
function DropdownForSchool(updateMode) {
    rf.ui.remoteBind('#DistrictCode', '/control/ctddl/AllDistrict/DistrictName/DistrictCode', {}, {}, undefined, 'change', function () {
        rf.ui.remoteBind('#UpazilaCode', '/control/ctddlcasede/AllUpazila/UpazilaName/UpazilaCode/' + JSON.stringify({ "DistrictCode": "" + $("#DistrictCode").val() }), {}, {}, undefined, 'change', function () {
            rf.ui.remoteBind('#UnionCode', '/control/ctddlcasede/AllUnion/UnionName/UnionCode/' + JSON.stringify({ "DistrictCode": "" + $("#DistrictCode").val(), "UpazilaCode": "" + $("#UpazilaCode").val() }), {}, {}, undefined, 'change', function () {
                rf.ui.remoteBind('#VillageCode', '/control/ctddlcasede/VillageInfo/VillageName/VillageCode/' + JSON.stringify({ "DistrictCode": "" + $("#DistrictCode").val(), "UpazilaCode": "" + $("#UpazilaCode").val(), "UnionCode": "" + $("#UnionCode").val() }), {}, {}, undefined, 'change', function () {
                }, updateMode, true);
            }, updateMode, true);
        }, updateMode, true);
    }, updateMode, true);
}


