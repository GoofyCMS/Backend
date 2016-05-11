/// <binding AfterBuild='visual-studio-copy:component-dlls' />

var gulp = require('gulp'),
    concat = require('gulp-concat');

var paths = {
    wwwroot: './wwwroot'
}
paths.artifactsBinDirectory = './../../artifacts/bin';
paths.componentsTempOutputFolder = "./temp/plugins";
paths.componentsOutputFolder = './plugins';

var externalAssemblies = [
                  //'Goofy.Application.Plugins',
                  //'Goofy.Application.Plugins.DTO',
                  //'Goofy.Domain.Plugins',
                  //'Goofy.Infrastructure.Plugins.Adapter',
                  //'Goofy.Infrastructure.Plugins.Data'
];
var runtimes = ['net451'];
var compModes = ['Debug'];


String.prototype.format = function () {
    var args = [].slice.call(arguments);
    return this.replace(/(\{\d+\})/g, function (a) {
        return args[+(a.substr(1, a.length - 2)) || 0];
    });
};

function getComponentDllPath(componentDll, compMode, runtime) {
    return '{0}/{1}/{2}/{3}/{1}.dll'.format(paths.artifactsBinDirectory, componentDll, compMode, runtime);
}

function getComponentPdbFilePath(componentName, runtime) {
    return '{0}/{1}/{2}/{3}/{1}.pdb'.format(paths.artifactsBinDirectory, componentName, "Debug", runtime);
}

function getComponentOutputFolder(componentName) {
    return '{0}/{1}'.format(paths.componentsOutputFolder, componentName);
}

function candidateAssemblies(compName)
{
    return [
                'Goofy.Application.' + compName,
                'Goofy.Domain.' + compName,
                'Goofy.Application.' + compName + '.DTO',
                'Goofy.Infrastructure.' + compName + '.Adapter',
                'Goofy.Infrastructure.' + compName + '.Data',
                'Goofy.Web.' + compName
           ];
}

function copyComponents() {
    for (var cmpIndex in externalAssemblies) {
        var componentName = externalAssemblies[cmpIndex];
        for (var rtmIndex in runtimes) {
            var runtime = runtimes[rtmIndex];
            for (var cmpModeIndex in compModes) {
                var cmpMode = compModes[cmpModeIndex];
                ///no se está teniendo en cuenta ni el runtime, ni el compMode para generar el output folder
                //console.info(componentName);
                //componentSource = getComponentDllPath(componentName, compModes)
                var candidates = candidateAssemblies(componentName);
                for (var compAssemblyIndex in candidates) {
                    compAssembly = candidates[compAssemblyIndex];
                    console.info(compAssembly);
                    gulp.src(getComponentDllPath(compAssembly, cmpMode, runtime))
                            .pipe(gulp.dest(getComponentOutputFolder(componentName)));
                    if (cmpMode == "Debug") {
                        //copiar los .pdb también
                        gulp.src(getComponentPdbFilePath(compAssembly, runtime))
                            .pipe(gulp.dest(getComponentOutputFolder(componentName)));
                    }
                }
            }
        }
    }
}

/*Este se usa para visual studio que da las salidas en artifacts/bin/..
y de ahí se compian para la carpeta componentes de donde las carga el framework
*/
gulp.task('visual-studio-copy:component-dlls', function (done) {
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