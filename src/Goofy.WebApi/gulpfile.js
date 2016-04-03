/// <binding AfterBuild='visual-studio-copy:component-dlls' />

var gulp = require('gulp'),
    concat = require('gulp-concat');

var paths = {
    wwwroot: './wwwroot'
}
paths.artifactsBinDirectory = './../../artifacts/bin';
paths.componentsTempOutputFolder = "./temp/components";
paths.componentsOutputFolder = './components';

var components = [
                  'Goofy.Component.Auth', 
                  'Goofy.Component.CorsIntegration',
                  'Goofy.Component.ComponentsWebInterface'
                 ];
var runtimes = ['net451'];
var compModes = ['Debug'];
                  

String.prototype.format = function () {
        var args = [].slice.call(arguments);
        return this.replace(/(\{\d+\})/g, function (a){
            return args[+(a.substr(1,a.length-2))||0];
        });
};

function getComponentDllPath(componentName, compMode, runtime) {
	return '{0}/{1}/{2}/{3}/{1}.dll'.format(paths.artifactsBinDirectory, componentName, compMode, runtime);
}

function getComponentOutputFolder(componentName) {
	return '{0}/{1}'.format(paths.componentsOutputFolder, componentName);
}

function copyComponents() {
    for (var cmp in components) {
        for (var rtm in runtimes) {
            for (var cmpMode in compModes) {
                ///no se está teniendo en cuenta ni el runtime, ni el compMode para generar el output folder
                gulp.src(getComponentDllPath(cmp, cmpMode, rtm))
                        .pipe(gulp.dest(getComponentOutputFolder(cmp)));
            }
         }
    }
}

/*Este se usa para visual studio que da las salidas en artifacts/bin/..
y de ahí se compian para la carpeta componentes de donde las carga el framework
*/
gulp.task('visual-studio-copy:component-dlls', function(done){
    copyComponents();
    done();
});

//vs code tasks
///lo ideal aquí sería sacar los runtimes de project.json y compilar para cada runtime
//gulp.task('vs-code-build-components', function(done){
//    for (var cmp in components) {
//        var componetFiles = getComponentsCsFiles(cmp);
//        gulp.src(componetFiles)
//            .pipe((['--fullpaths', '--debug', '-target:exe', '-out:.']));
//    }
//    done();
//});

//function getComponentsCsFiles(componentName){
//    return './../{0}/**/*.cs'.format(componentName);
//}