var counter = 2;


function decrement() {
    if (--counter == 0) {
        $(".kickstart-loading").hide();
    }
}

function loadProjects(mid, pageNo, recordsCount, sortCol, isVisible, isDeleted, createdBy, leadBy, participantId, success) {

    var sf = $.ServicesFramework(mid);
    counter = 2;
    setTimeout(decrement, 500);
    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/GetProjectList',
        beforeSend: function(xhr) {
            showLoading();
            sf.setModuleHeaders(xhr);            
        },
        data: { PageNo: pageNo, RecordsPerPage: recordsCount, SortCol: sortCol, IsVisible: isVisible, IsDeleted: isDeleted, CreatedBy: createdBy, LeadBy: leadBy, ParticipantId: participantId },
        success: success,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function showLoading() {
    $(".kickstart-loading").show();
}

function loadProjectsPaging(mid, recordsCount, success) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/GetProjectPagingControl',
        beforeSend: sf.setModuleHeaders,
        data: { RecordsPerPage: recordsCount },
        success: success,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function loadProject(mid,projectId,sucess) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/GetProject',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid },
        success: sucess,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function loadProjectStatus(mid, projectId, sucess) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/GetProjectStatus',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid },
        success: sucess,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function likeProject(mid, projectId, sucess) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/LikeProject',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid },
        success: sucess,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function unlikeProject(mid, projectId, sucess) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/UnLikeProject',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid },
        success: sucess,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function addComment(mid, projectId, comment, parent, sucess) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/AddComment',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid, Comment: comment, ParentId: parent },
        success: sucess,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function editComment(mid, projectId, comment, commentid, sucess) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/EditComment',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid, Comment: comment, CommentId: commentid },
        success: sucess,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function deleteComment(mid, projectId, commentid, sucess) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/DeleteComment',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid, CommentId: commentid },
        success: sucess,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function likeComment(mid, projectId, id, sucess) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/LikeComment',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid, CommentId: id },
        success: sucess,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function unlikeComment(mid, projectId, id, sucess) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/UnLikeComment',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid, CommentId: id },
        success: sucess,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function setProjectVisibilityStatus(mid, pid, isVisible, result) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/SetProjectVisibilityStatus',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid, IsVisible: isVisible },
        success: result,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function setProjectLockStatus(mid, pid, isLocked, result) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/SetProjectLockStatus',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid, IsLocked: isLocked },
        success: result,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function setProjectDeletedStatus(mid, pid, isDeleted, result) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/SetProjectDeletedStatus',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid, IsDeleted: isDeleted },
        success: result,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function changeReleaseSubscriptionStatus(mid, pid, result) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/ChangeReleaseSubscriptionStatus',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid },
        success: result,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function changeCommentSubscriptionStatus(mid, pid, result) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/ChangeCommentSubscriptionStatus',
        beforeSend: sf.setModuleHeaders,
        data: { ProjectId: pid },
        success: result,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function changeProjectListSubscriptionStatus(mid, result) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "POST",
        url: sf.getServiceRoot('Connect/Kickstart') + 'Kickstart/ChangeProjectListSubscriptionStatus',
        beforeSend: sf.setModuleHeaders,
        success: result,
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function disableLink(link) {
    $(link).addClass('disabled');
    $(link).html(waitText);
}

function enableLink(link, text) {
    $(link).removeClass('disabled');
    $(link).html(text);
}