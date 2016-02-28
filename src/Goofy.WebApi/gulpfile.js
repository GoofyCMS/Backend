/// <binding AfterBuild='copy' />

var gulp = require('gulp'),
    concat = require('gulp-concat');

var paths = {
    wwwroot: './wwwroot'
}
paths.artifactsBinDirectory = './../../artifacts/bin';
paths.componentsOutputFolder = './components';

var compMode = 'Debug';
var runtime = 'dnx451'

var authComponent = 'Goofy.Component.Auth';
var corsComponent = 'Goofy.Component.CorsIntegration';
var compInterfaceComponent = 'Goofy.Component.ComponentsWebInterface';
var testComponent = 'Goofy.Component.ControllersAndRoutes';

String.prototype.format = function () {
        var args = [].slice.call(arguments);
        return this.replace(/(\{\d+\})/g, function (a){
            return args[+(a.substr(1,a.length-2))||0];
        });
};

function getComponentDllPath(componentName) {
	return '{0}/{1}/{2}/{3}/{1}.dll'.format(paths.artifactsBinDirectory, componentName, compMode, runtime);
}

function getComponentOutputFolder(componentName) {
	return '{0}/{1}'.format(paths.componentsOutputFolder, componentName);
}


gulp.task('copy:authComponent', function () {
    return gulp.src([getComponentDllPath(authComponent)])
               .pipe(gulp.dest(getComponentOutputFolder(authComponent)));
});

gulp.task('copy:corsComponent', function () {
    return gulp.src([getComponentDllPath(corsComponent)])
               .pipe(gulp.dest(getComponentOutputFolder(corsComponent)));
});

gulp.task('copy:compInterfaceComponent', function () {
    return gulp.src([getComponentDllPath(compInterfaceComponent)])
               .pipe(gulp.dest(getComponentOutputFolder(compInterfaceComponent)));
});

gulp.task('copy:test', function () {
    return gulp.src([getComponentDllPath(testComponent)])
               .pipe(gulp.dest(getComponentOutputFolder(testComponent)));
});

gulp.task('copy', ['copy:authComponent', 'copy:corsComponent', 'copy:compInterfaceComponent', 'copy:test']);