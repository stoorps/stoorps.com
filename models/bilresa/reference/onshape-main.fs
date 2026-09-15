FeatureScript 3070;
import(path : "onshape/std/geometry.fs", version : "3070.0");

annotation { "Default Units" : ["cubicMillimeter", "degree", "degreePerSecond", "footPoundForce", "hertz", "inchPound", "millimeter", "millimeterPerSecondSquared", "pound", "poundForce", "poundPerCubicInch", "poundPerSquareInch", "second", "squareMillimeter"] }
export function main()
{
    return build({});
}

export function build()
{
    return buildPrivate({});
}

export function build(configuration is map)
{
    return buildPrivate(configuration);
}

const buildPrivate = definePartStudio(function(context is Context, configuration is map, lookup is function)
    precondition
    {
    }
    {
        const id is Id = newId();
        annotation { 'unused' : true }
        var features = {};
        features.Fouwi28iyGK3kZ6 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "Fouwi28iyGK3kZ6", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.NUMBER, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "num_switches_left", "lengthValue" : 0.0 * meter, "angleValue" : 0.0 * degree, "numberValue" : { 'value' : try(1), 'expression' : "1" }.value, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(1), 'expression' : "1" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.Fouwi28iyGK3kZ6(id));
        features.FTFoeRWAXDBbmyV = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FTFoeRWAXDBbmyV", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.NUMBER, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "num_switches_right", "lengthValue" : 0.0 * meter, "angleValue" : 0.0 * degree, "numberValue" : { 'value' : try(1), 'expression' : "1" }.value, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(1), 'expression' : "1" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FTFoeRWAXDBbmyV(id));
        features.Fdok3fQFlXBkKQO = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "Fdok3fQFlXBkKQO", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "switch_pitch", "lengthValue" : { 'value' : try(4 * millimeter), 'expression' : "4 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(4 * millimeter), 'expression' : "4 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.Fdok3fQFlXBkKQO(id));
        features.FPMNeJgIXhx45Zv = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FPMNeJgIXhx45Zv", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "switch_width", "lengthValue" : { 'value' : try(39.85 * millimeter), 'expression' : "39.85 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(39.85 * millimeter), 'expression' : "39.85 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FPMNeJgIXhx45Zv(id));
        features.FpLL0GnwTqbQr8T = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FpLL0GnwTqbQr8T", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "switch_height", "lengthValue" : { 'value' : try(70 * millimeter), 'expression' : "70 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(70 * millimeter), 'expression' : "70 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FpLL0GnwTqbQr8T(id));
        features.FnTANSt7heFEWDw = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FnTANSt7heFEWDw", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "switch_depth", "lengthValue" : { 'value' : try(18 * millimeter), 'expression' : "18 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(18 * millimeter), 'expression' : "18 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FnTANSt7heFEWDw(id));
        features.FWOMzjzDNG4yKo5 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FWOMzjzDNG4yKo5", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "switch_tolerance", "lengthValue" : { 'value' : try(0.1 * millimeter), 'expression' : "0.1 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(0.1 * millimeter), 'expression' : "0.1 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FWOMzjzDNG4yKo5(id));
        features.F5pmyjk92YJlOu7 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "F5pmyjk92YJlOu7", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "mag_width", "lengthValue" : { 'value' : try(16.93 * millimeter), 'expression' : "16.93 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(16.93 * millimeter), 'expression' : "16.93 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.F5pmyjk92YJlOu7(id));
        features.FkzfcGbFEhuoBlf = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FkzfcGbFEhuoBlf", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "mag_height", "lengthValue" : { 'value' : try(46.88 * millimeter), 'expression' : "46.88 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(46.88 * millimeter), 'expression' : "46.88 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FkzfcGbFEhuoBlf(id));
        features.FsglFIDTK4D0cap = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FsglFIDTK4D0cap", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "mag_thickness", "lengthValue" : { 'value' : try(0.56 * millimeter), 'expression' : "0.56 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(0.56 * millimeter), 'expression' : "0.56 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FsglFIDTK4D0cap(id));
        features.FaFbdPViNl4RVk1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FaFbdPViNl4RVk1", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "mag_xy_tolerance", "lengthValue" : { 'value' : try(0.01 * millimeter), 'expression' : "0.01 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(0.01 * millimeter), 'expression' : "0.01 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FaFbdPViNl4RVk1(id));
        features.Fy74yLPcQHWJDJe = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "Fy74yLPcQHWJDJe", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "mag_adhesive_tolerance", "lengthValue" : { 'value' : try(0.3 * millimeter), 'expression' : "0.3 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(0.3 * millimeter), 'expression' : "0.3 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "NEEDS MEASURING" });
                }
            };
        try(features.Fy74yLPcQHWJDJe(id));
        features.FoxWtg5xW7biRci = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FoxWtg5xW7biRci", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_width", "lengthValue" : { 'value' : try(85.60 * millimeter), 'expression' : "85.60 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(85.60 * millimeter), 'expression' : "85.60 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FoxWtg5xW7biRci(id));
        features.FgTfyiPuvNslX8o = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FgTfyiPuvNslX8o", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_height", "lengthValue" : { 'value' : try(85.35 * millimeter), 'expression' : "85.35 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(85.35 * millimeter), 'expression' : "85.35 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FgTfyiPuvNslX8o(id));
        features.FkQLUoiGJ15x5E8 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FkQLUoiGJ15x5E8", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_depth", "lengthValue" : { 'value' : try(8.19 * millimeter), 'expression' : "8.19 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(8.19 * millimeter), 'expression' : "8.19 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "NEG TOL!!!" });
                }
            };
        try(features.FkQLUoiGJ15x5E8(id));
        features.F37nMnDwIlCYvcK = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "F37nMnDwIlCYvcK", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_tolerance", "lengthValue" : { 'value' : try(0.2 * millimeter), 'expression' : "0.2 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(0.2 * millimeter), 'expression' : "0.2 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.F37nMnDwIlCYvcK(id));
        features.FJIDfG1YSk1m1Pn = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FJIDfG1YSk1m1Pn", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_cluster_height", "lengthValue" : { 'value' : try(27.45 * millimeter), 'expression' : "27.45 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(27.45 * millimeter), 'expression' : "27.45 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FJIDfG1YSk1m1Pn(id));
        features.F0K5PyGYJDkEGFs = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "F0K5PyGYJDkEGFs", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_cluster_width", "lengthValue" : { 'value' : try(31.9 * millimeter), 'expression' : "31.9 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(31.9 * millimeter), 'expression' : "31.9 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.F0K5PyGYJDkEGFs(id));
        features.Fg9BvYzAPKIshYh = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "Fg9BvYzAPKIshYh", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_cluster_depth", "lengthValue" : { 'value' : try(6.6 * millimeter), 'expression' : "6.6 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(6.6 * millimeter), 'expression' : "6.6 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.Fg9BvYzAPKIshYh(id));
        features.FLxiUskK5uamwgc = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FLxiUskK5uamwgc", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_cluster_tolerance", "lengthValue" : { 'value' : try(0.01 * millimeter), 'expression' : "0.01 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(0.01 * millimeter), 'expression' : "0.01 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FLxiUskK5uamwgc(id));
        features.F6R41CoPSSq3S7N = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "F6R41CoPSSq3S7N", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_screw_offset_x", "lengthValue" : { 'value' : try(11.69 * millimeter), 'expression' : "11.69 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(11.69 * millimeter), 'expression' : "11.69 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "From edges" });
                }
            };
        try(features.F6R41CoPSSq3S7N(id));
        features.FWHgoHNy1lR0KlY = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FWHgoHNy1lR0KlY", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_screw_offset_y", "lengthValue" : 0.0 * meter, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(0), 'expression' : "0" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "From center" });
                }
            };
        try(features.FWHgoHNy1lR0KlY(id));
        features.FtFF3CXC0QdfXgh = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FtFF3CXC0QdfXgh", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_screw_head_d", "lengthValue" : { 'value' : try(6.3 * millimeter), 'expression' : "6.3 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(6.3 * millimeter), 'expression' : "6.3 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FtFF3CXC0QdfXgh(id));
        features.F1aQBYj3DGK1dtw = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "F1aQBYj3DGK1dtw", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_screw_shaft_d", "lengthValue" : { 'value' : try(3.1 * millimeter), 'expression' : "3.1 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(3.1 * millimeter), 'expression' : "3.1 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.F1aQBYj3DGK1dtw(id));
        features.FFozxjp6KMB3l4Q = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FFozxjp6KMB3l4Q", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "sp_screw_shaft_tol", "lengthValue" : { 'value' : try(0.2 * millimeter), 'expression' : "0.2 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(0.2 * millimeter), 'expression' : "0.2 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FFozxjp6KMB3l4Q(id));
        features.FUqMA0HAfJocndz = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FUqMA0HAfJocndz", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "bracket_rail_z_offset", "lengthValue" : { 'value' : try(3.5 * millimeter), 'expression' : "3.5 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(3.5 * millimeter), 'expression' : "3.5 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FUqMA0HAfJocndz(id));
        features.FEUaZ9GJuwYttCL = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FEUaZ9GJuwYttCL", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "bracket_rail_z_thickness", "lengthValue" : { 'value' : try(2.5 * millimeter), 'expression' : "2.5 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(2.5 * millimeter), 'expression' : "2.5 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FEUaZ9GJuwYttCL(id));
        features.FgIlXQuz97pPVYE = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FgIlXQuz97pPVYE", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "bracket_rail_stopper_relief", "lengthValue" : { 'value' : try(3 * millimeter), 'expression' : "3mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(3 * millimeter), 'expression' : "3mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FgIlXQuz97pPVYE(id));
        features.FhRvTcPMbJVEy5k = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FhRvTcPMbJVEy5k", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "bracket_rail_width", "lengthValue" : { 'value' : try(3 * millimeter), 'expression' : "3 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(3 * millimeter), 'expression' : "3 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FhRvTcPMbJVEy5k(id));
        features.F8r8tibxmgT731W = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "F8r8tibxmgT731W", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "bracket_rail_tolerance", "lengthValue" : { 'value' : try(0.2 * millimeter), 'expression' : "0.2 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(0.2 * millimeter), 'expression' : "0.2 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.F8r8tibxmgT731W(id));
        features.FCu8gZ4Ig4Ef2qc = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FCu8gZ4Ig4Ef2qc", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_wall_thickness", "lengthValue" : { 'value' : try(2.5 * millimeter), 'expression' : "2.5 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(2.5 * millimeter), 'expression' : "2.5 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FCu8gZ4Ig4Ef2qc(id));
        features.Fa5gAlOszE2vE6B = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "Fa5gAlOszE2vE6B", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_left_switch_w_total", "lengthValue" : { 'value' : try(((lookup('switch_width') + (lookup('switch_tolerance') * 2) + lookup('switch_pitch')) * lookup('num_switches_left')) + lookup('switch_pitch')), 'expression' : "((#switch_width + (#switch_tolerance * 2) + #switch_pitch) * #num_switches_left) + #switch_pitch" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(((lookup('switch_width') + (lookup('switch_tolerance') * 2) + lookup('switch_pitch')) * lookup('num_switches_left')) + lookup('switch_pitch')), 'expression' : "((#switch_width + (#switch_tolerance * 2) + #switch_pitch) * #num_switches_left) + #switch_pitch" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.Fa5gAlOszE2vE6B(id));
        features.FWHYoGSP0e1A13d = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FWHYoGSP0e1A13d", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_right_switch_w_total", "lengthValue" : { 'value' : try(((lookup('switch_width') + (lookup('switch_tolerance') * 2) + lookup('switch_pitch')) * lookup('num_switches_right')) + lookup('switch_pitch')), 'expression' : "((#switch_width + (#switch_tolerance * 2) + #switch_pitch) * #num_switches_right) + #switch_pitch" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(((lookup('switch_width') + (lookup('switch_tolerance') * 2) + lookup('switch_pitch')) * lookup('num_switches_right')) + lookup('switch_pitch')), 'expression' : "((#switch_width + (#switch_tolerance * 2) + #switch_pitch) * #num_switches_right) + #switch_pitch" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FWHYoGSP0e1A13d(id));
        features.FHX11tn5ONNUOqH = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FHX11tn5ONNUOqH", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_width", "lengthValue" : { 'value' : try((lookup('sp_width') + (lookup('sp_tolerance') + lookup('asm_wall_thickness') + lookup('bracket_rail_width') + lookup('bracket_rail_tolerance') + lookup('asm_wall_thickness')) * 2) + lookup('asm_left_switch_w_total') + lookup('asm_right_switch_w_total')), 'expression' : "(#sp_width + (#sp_tolerance + #asm_wall_thickness + #bracket_rail_width + #bracket_rail_tolerance  + #asm_wall_thickness) *2) + #asm_left_switch_w_total + #asm_right_switch_w_total" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try((lookup('sp_width') + (lookup('sp_tolerance') + lookup('asm_wall_thickness') + lookup('bracket_rail_width') + lookup('bracket_rail_tolerance') + lookup('asm_wall_thickness')) * 2) + lookup('asm_left_switch_w_total') + lookup('asm_right_switch_w_total')), 'expression' : "(#sp_width + (#sp_tolerance + #asm_wall_thickness + #bracket_rail_width + #bracket_rail_tolerance  + #asm_wall_thickness) *2) + #asm_left_switch_w_total + #asm_right_switch_w_total" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FHX11tn5ONNUOqH(id));
        features.Fo2If3tlunrxeCz = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "Fo2If3tlunrxeCz", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_height", "lengthValue" : { 'value' : try(lookup('sp_height') + ((lookup('sp_tolerance') + lookup('asm_wall_thickness')) * 2)), 'expression' : "#sp_height + ((#sp_tolerance + #asm_wall_thickness)*2)" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(lookup('sp_height') + ((lookup('sp_tolerance') + lookup('asm_wall_thickness')) * 2)), 'expression' : "#sp_height + ((#sp_tolerance + #asm_wall_thickness)*2)" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.Fo2If3tlunrxeCz(id));
        features.FxxA7iAcRzTG6XD = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FxxA7iAcRzTG6XD", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_sp_clearance", "lengthValue" : { 'value' : try(lookup('sp_depth') + lookup('sp_tolerance')), 'expression' : "#sp_depth + #sp_tolerance" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(lookup('sp_depth') + lookup('sp_tolerance')), 'expression' : "#sp_depth + #sp_tolerance" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FxxA7iAcRzTG6XD(id));
        features.FNezCXLKBhbQxaG = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FNezCXLKBhbQxaG", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_cluster_clearance", "lengthValue" : { 'value' : try(lookup('asm_sp_clearance') + lookup('sp_cluster_depth') + lookup('sp_cluster_tolerance')), 'expression' : "#asm_sp_clearance + #sp_cluster_depth + #sp_cluster_tolerance" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(lookup('asm_sp_clearance') + lookup('sp_cluster_depth') + lookup('sp_cluster_tolerance')), 'expression' : "#asm_sp_clearance + #sp_cluster_depth + #sp_cluster_tolerance" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FNezCXLKBhbQxaG(id));
        features.FDBXWIh8W6qJNht = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FDBXWIh8W6qJNht", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_total_depth", "lengthValue" : { 'value' : try(lookup('switch_depth') + lookup('switch_tolerance') + (lookup('asm_wall_thickness') * 2)), 'expression' : "#switch_depth + #switch_tolerance + (#asm_wall_thickness *2)" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(lookup('switch_depth') + lookup('switch_tolerance') + (lookup('asm_wall_thickness') * 2)), 'expression' : "#switch_depth + #switch_tolerance + (#asm_wall_thickness *2)" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FDBXWIh8W6qJNht(id));
        features.F9whUSIAeiGzznP = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "F9whUSIAeiGzznP", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_cover_section_depth", "lengthValue" : { 'value' : try(lookup('asm_cluster_clearance') + lookup('sp_cluster_tolerance')), 'expression' : "#asm_cluster_clearance + #sp_cluster_tolerance" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(lookup('asm_cluster_clearance') + lookup('sp_cluster_tolerance')), 'expression' : "#asm_cluster_clearance + #sp_cluster_tolerance" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.F9whUSIAeiGzznP(id));
        features.FM4TVltkObl9H11 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FM4TVltkObl9H11", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "cover_tolerance", "lengthValue" : { 'value' : try(0.05 * millimeter), 'expression' : "0.05 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(0.05 * millimeter), 'expression' : "0.05 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FM4TVltkObl9H11(id));
        features.FaeYlHJH8sTlucY = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FaeYlHJH8sTlucY", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "cover_if_depth", "lengthValue" : { 'value' : try(1.5 * millimeter), 'expression' : "1.5 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(1.5 * millimeter), 'expression' : "1.5 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FaeYlHJH8sTlucY(id));
        features.Foq4mzemFzBo8tP = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "Foq4mzemFzBo8tP", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "cover_if_extension", "lengthValue" : { 'value' : try(lookup('cover_if_depth') + 0.125 * millimeter), 'expression' : "#cover_if_depth + 0.125mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(lookup('cover_if_depth') + 0.125 * millimeter), 'expression' : "#cover_if_depth + 0.125mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.Foq4mzemFzBo8tP(id));
        features.FVs9cd2oSNlD0KP_0 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    const initialGuessFVs9cd2oSNlD0KP_0 = { "dZ52UiFd4O3Z.left" : [-0.13455000000000003, -0.009182758924961091, 6.123233995736766E-17, -1.0, -0.050662570627734896, 0.03229705277781275], "dZ52UiFd4O3Z.middle" : [0.0, 0.0], "icpN5dRRYMN3.bottom" : [-0.007900628052949894, 0.042685, 1.0, 0.0, -0.03490937194705011, 0.050710628052949895], "icpN5dRRYMN3.top" : [0.0, -0.042685, 1.0, 0.0, -0.04281, 0.04281], "icpN5dRRYMN3.left" : [-0.04281, -0.009000008039474483, 0.0, -1.0, -0.051685008039474484, 0.03368499196052552], "icpN5dRRYMN3.right" : [0.04281, -0.0090000080394745, 0.0, -1.0, -0.0516850080394745, 0.033684991960525504], "icpN5dRRYMN3.middle" : [0.0, 0.0], "AGiFvdYnC1Gj" : [-0.04281, 0.0439325, 0.0, 1.0, -0.0012474999999999986, 0.0012524999999999967], "GuM1oInfJLRW" : [0.04281, 0.0439325, 0.0, 1.0, -0.0012474999999999986, 0.0012524999999999967], "CKUJZR0utnep" : [0.04475425806462697, 0.042685, 1.0, 0.0, -0.0019442580646269678, 0.0011557419353730239], "ggliDtHZBie1" : [-0.044768297820093235, 0.042685, -1.0, 0.0, -0.001958297820093234, 0.0011417021799067645], "8iArdl5WzThi" : [0.04590999999999999, 0.043935, 0.0, 1.0, -0.0012500000000000011, 0.0012499999999999942], "ZW8XGf4wY7W4" : [-0.04591, 0.043935, 0.0, 1.0, -0.0012500000000000011, 0.0012499999999999942], "WE6aMD0pgltb" : [-0.04281, -0.043935, 0.0, -1.0, -0.0012500000000000011, 0.0012499999999999942], "GQzM1rs8KZsm" : [0.04281, -0.043935, 0.0, -1.0, -0.0012500000000000011, 0.0012499999999999942], "R4fthY4SaoUb" : [0.13455, 2.1490404183402674E-18, 6.123233995736766E-17, -1.0, -0.04518500000000002, 0.045184999999999975], "E5psPBdazMi6" : [-0.043870000000000034, -0.045184999999999996, -1.0, -1.2246467991473532E-16, -0.17842000000000002, 0.09068], "qi2Ta53CvWH1" : [-0.13455000000000003, -0.043332405851386924, 0.0, 1.0, -0.0018525941486130793, 0.0018525941486130793], "EFh1IGCAhwQi" : [-0.043359536788463625, 0.045184999999999996, -1.0, -1.2246467991473535E-16, -0.0911904632115364, 0.0911904632115364], "tTOCUaFlARpN" : [-0.13455000000000003, 0.04333240585138689, 1.2246467991473532E-16, -1.0, -0.0018525941486130931, 0.0018525941486130862], "D9r2EV7pIqlj.bottom" : [0.019074290250688793, 0.035015000000000004, -1.0, -1.2246467991473532E-16, -0.0237357097493112, 0.016134290250688813], "D9r2EV7pIqlj.top" : [0.019074290250688793, -0.03500500000000001, -1.0, -1.2246467991473532E-16, -0.023735709749311208, 0.01613429025068879], "D9r2EV7pIqlj.left" : [0.04281, 3.469446951953614E-18, 0.0, -1.0, -0.035015000000000004, 0.03500500000000001], "D9r2EV7pIqlj.right" : [0.0029399999999999912, 1.142183437462556E-18, 3.061616997868383E-16, -1.0, -0.035015000000000004, 0.03500500000000001], "WxDR8351T9aN.top.start.orphan" : [0.03921358050137758, -0.03500500000000001], "WxDR8351T9aN.bottom.start.orphan" : [0.03921358050137758, 0.035015000000000004], "jgc1xXvtFeQw" : [0.022874999999999993, 4.9999999999980616E-6, 1.981974836877243E-16, -1.0, -0.035010000000000006, 0.035010000000000006], "Cxk5xPmxKsRz.bottom" : [0.02201659236252308, -0.023450000000000006, -1.0, -1.2246467991473532E-16, -0.00933340763747691, 0.007616592362523088], "Cxk5xPmxKsRz.top" : [0.022874999999999993, 0.02345, -1.0, -1.2246467991473532E-16, -0.008474999999999996, 0.008474999999999998], "Cxk5xPmxKsRz.left" : [0.03134999999999999, -0.005345327568054206, 6.123233995736765E-17, 1.0, -0.0181046724319458, 0.028795327568054203], "Cxk5xPmxKsRz.right" : [0.014399999999999993, -0.005345327568054205, 6.123233995736765E-17, 1.0, -0.0181046724319458, 0.028795327568054203], "Cxk5xPmxKsRz.middle" : [0.022874999999999993, 0.0], "W5o7WVF6PNS6.0.1.0" : [0.0629442902506888, 0.035015000000000004, -1.0, -1.2246467991473532E-16, -0.0237357097493112, 0.016134290250688813], "W5o7WVF6PNS6.1.1.0" : [0.04681, 1.142183437462556E-18, 3.061616997868383E-16, -1.0, -0.035015000000000004, 0.03500500000000001], "W5o7WVF6PNS6.2.1.0" : [0.0629442902506888, -0.03500500000000001, -1.0, -1.2246467991473532E-16, -0.023735709749311208, 0.01613429025068879], "W5o7WVF6PNS6.3.1.0" : [0.06588659236252309, -0.023450000000000006, -1.0, -1.2246467991473532E-16, -0.00933340763747691, 0.007616592362523088], "W5o7WVF6PNS6.4.1.0" : [0.05827, -0.005345327568054205, 6.123233995736765E-17, 1.0, -0.0181046724319458, 0.028795327568054203], "W5o7WVF6PNS6.5.1.0" : [0.07522, -0.005345327568054206, 6.123233995736765E-17, 1.0, -0.0181046724319458, 0.028795327568054203], "W5o7WVF6PNS6.6.1.0" : [0.066745, 0.02345, -1.0, -1.2246467991473532E-16, -0.008474999999999996, 0.008474999999999998], "W5o7WVF6PNS6.7.1.0" : [0.08668000000000001, 3.469446951953614E-18, 0.0, -1.0, -0.035015000000000004, 0.03500500000000001], "W5o7WVF6PNS6.direction1" : [0.024875000000000008, -0.03500500000000001, 1.0, 0.0, -0.021935000000000007, 0.021935000000000003], "1h5lzhu1DqW3.bottom" : [-0.024152792517766357, 0.035015000000000004, 1.0, 0.0, -0.018657207482233644, 0.02121279251776635], "1h5lzhu1DqW3.top" : [-0.024152792517766357, -0.03500500000000001, 1.0, 0.0, -0.018657207482233644, 0.021212792517766355], "1h5lzhu1DqW3.left" : [-0.04281, 4.9999999999980616E-6, 0.0, -1.0, -0.035010000000000006, 0.035010000000000006], "1h5lzhu1DqW3.right" : [-0.002940000000000005, 4.999999999998218E-6, 6.123233995736766E-17, -1.0, -0.03500999999999999, 0.03501000000000001], "tgSFU2ntIwoe" : [-0.022875000000000003, 4.9999999999980616E-6, 9.909874184386215E-17, -1.0, -0.035010000000000006, 0.035010000000000006], "vNZaYuuZX5WG.bottom" : [-0.022398845253884785, 0.023454999999999997, -1.0, 0.0, -0.007998845253884787, 0.008951154746115218], "vNZaYuuZX5WG.top" : [-0.022875000000000003, -0.023445000000000008, -1.0, 0.0, -0.008475000000000002, 0.008475], "vNZaYuuZX5WG.left" : [-0.0144, 2.5295673608778955E-4, -6.123233995736766E-17, -1.0, -0.023202043263912207, 0.023697956736087798], "vNZaYuuZX5WG.right" : [-0.03135, 2.529567360877906E-4, -6.123233995736766E-17, -1.0, -0.023202043263912207, 0.023697956736087798], "vNZaYuuZX5WG.middle" : [-0.022875000000000003, 4.9999999999980616E-6], "gAMrxUh9DdFH.0.1.0" : [-0.06745279251776634, 0.035015000000000004, 1.0, 0.0, -0.019227207482233666, 0.020642792517766337], "gAMrxUh9DdFH.1.1.0" : [-0.08668000000000001, 4.999999999998027E-6, 6.123233995736766E-17, -1.0, -0.03500999999999997, 0.03501000000000002], "gAMrxUh9DdFH.2.1.0" : [-0.06745279251776634, -0.03500500000000001, 1.0, 0.0, -0.019227207482233652, 0.02064279251776635], "gAMrxUh9DdFH.3.1.0" : [-0.04681, 4.9999999999981835E-6, 6.123233995736766E-17, -1.0, -0.03501, 0.03501000000000001], "gAMrxUh9DdFH.4.1.0" : [-0.058269999999999995, 2.529567360877896E-4, -6.123233995736766E-17, -1.0, -0.023202043263912193, 0.02369795673608781], "gAMrxUh9DdFH.5.1.0" : [-0.066175, -0.023445000000000008, -1.0, 0.0, -0.007905000000000002, 0.009044999999999997], "gAMrxUh9DdFH.6.1.0" : [-0.07522, 2.5295673608779064E-4, -6.123233995736766E-17, -1.0, -0.023202043263912193, 0.02369795673608781], "gAMrxUh9DdFH.7.1.0" : [-0.06569884525388478, 0.023454999999999997, -1.0, 0.0, -0.007428845253884785, 0.009521154746115215], "gAMrxUh9DdFH.direction1" : [-0.06445999999999999, -0.03500500000000001, -1.0, 0.0, -0.02164999999999999, 0.022220000000000004], "W5o7WVF6PNS6.3.2.0" : [0.09068000000000001, 1.142183437462556E-18, 3.061616997868383E-16, -1.0, -0.035015000000000004, 0.03500500000000001], "W5o7WVF6PNS6.12.2.0" : [0.10214000000000002, -0.005345327568054205, 6.123233995736765E-17, 1.0, -0.0181046724319458, 0.028795327568054203], "W5o7WVF6PNS6.18.2.0" : [0.11061500000000002, 0.02345, -1.0, -1.2246467991473532E-16, -0.008474999999999996, 0.008474999999999996], "W5o7WVF6PNS6.9.2.0" : [0.10975659236252311, -0.023450000000000006, -1.0, -1.2246467991473532E-16, -0.009333407637476906, 0.007616592362523086], "gAMrxUh9DdFH.12.2.0" : [-0.10214000000000001, 2.5295673608778955E-4, -6.123233995736766E-17, -1.0, -0.023202043263912207, 0.023697956736087798], "W5o7WVF6PNS6.0.2.0" : [0.10681429025068882, 0.035015000000000004, -1.0, -1.2246467991473532E-16, -0.023735709749311204, 0.016134290250688813], "gAMrxUh9DdFH.3.2.0" : [-0.13055, 4.9999999999980616E-6, 0.0, -1.0, -0.035010000000000006, 0.035010000000000006], "W5o7WVF6PNS6.6.2.0" : [0.10681429025068882, -0.03500500000000001, -1.0, -1.2246467991473532E-16, -0.023735709749311204, 0.0161342902506888], "gAMrxUh9DdFH.9.2.0" : [-0.09068000000000001, 4.999999999998218E-6, 6.123233995736766E-17, -1.0, -0.03500999999999999, 0.03501000000000001], "W5o7WVF6PNS6.15.2.0" : [0.11909000000000002, -0.005345327568054206, 6.123233995736765E-17, 1.0, -0.0181046724319458, 0.028795327568054203], "gAMrxUh9DdFH.0.2.0" : [-0.11189279251776638, 0.035015000000000004, 1.0, 0.0, -0.018657207482233623, 0.021212792517766352], "gAMrxUh9DdFH.6.2.0" : [-0.11189279251776638, -0.03500500000000001, 1.0, 0.0, -0.018657207482233623, 0.021212792517766366], "gAMrxUh9DdFH.15.2.0" : [-0.11061500000000002, -0.023445000000000008, -1.0, 0.0, -0.00847500000000001, 0.008474999999999996], "gAMrxUh9DdFH.21.2.0" : [-0.1101388452538848, 0.023454999999999997, -1.0, 0.0, -0.007998845253884793, 0.008951154746115214], "gAMrxUh9DdFH.18.2.0" : [-0.11909000000000002, 2.529567360877906E-4, -6.123233995736766E-17, -1.0, -0.023202043263912207, 0.023697956736087798], "W5o7WVF6PNS6.21.2.0" : [0.13055000000000003, 3.469446951953614E-18, 0.0, -1.0, -0.035015000000000004, 0.03500500000000001], "ilE1DLjavjuI" : [0.09119046321153639, 0.04518500000000001, 1.0, 1.6003155056198416E-16, -0.04335953678846361, 0.04335953678846362] };
                    {
                    }
                    var ijQbwythTwvaUC_query;
                    ijQbwythTwvaUC_query=qCompressed(1.0,"%B5$QueryM4Sa$entityTypeBa$EntityTypeS4$FACESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S3.7$TopplaneOpS9$queryTypeS5$DUMMY",id);
                    annotation { "Feature Name" : "asm_sketch" }
                    var sketch = newSketch(context, id + "FVs9cd2oSNlD0KP_0", { "sketchPlane" : qUnion([ijQbwythTwvaUC_query]), "disableImprinting" : false });
                    skLineSegment(sketch, "dZ52UiFd4O3Z.left", { "construction" : false, "index" : "3" });
                    skPoint(sketch, "dZ52UiFd4O3Z.middle", { "construction" : true, "index" : "1" });
                    skLineSegment(sketch, "icpN5dRRYMN3.bottom", { "construction" : false, "index" : "5" });
                    skLineSegment(sketch, "icpN5dRRYMN3.top", { "construction" : false, "index" : "6" });
                    skLineSegment(sketch, "icpN5dRRYMN3.left", { "construction" : false, "index" : "7" });
                    skLineSegment(sketch, "icpN5dRRYMN3.right", { "construction" : false, "index" : "8" });
                    skPoint(sketch, "icpN5dRRYMN3.middle", { "construction" : true, "index" : "2" });
                    skLineSegment(sketch, "AGiFvdYnC1Gj", { "construction" : false, "index" : "19" });
                    skLineSegment(sketch, "GuM1oInfJLRW", { "construction" : false, "index" : "20" });
                    skLineSegment(sketch, "CKUJZR0utnep", { "construction" : false, "index" : "29" });
                    skLineSegment(sketch, "ggliDtHZBie1", { "construction" : false, "index" : "30" });
                    skLineSegment(sketch, "8iArdl5WzThi", { "construction" : false, "index" : "31" });
                    skLineSegment(sketch, "ZW8XGf4wY7W4", { "construction" : false, "index" : "32" });
                    skLineSegment(sketch, "WE6aMD0pgltb", { "construction" : false, "index" : "33" });
                    skLineSegment(sketch, "GQzM1rs8KZsm", { "construction" : false, "index" : "34" });
                    skLineSegment(sketch, "R4fthY4SaoUb", { "construction" : false, "index" : "35" });
                    skLineSegment(sketch, "E5psPBdazMi6", { "construction" : false, "index" : "36" });
                    skLineSegment(sketch, "qi2Ta53CvWH1", { "construction" : false, "index" : "37" });
                    skLineSegment(sketch, "EFh1IGCAhwQi", { "construction" : false, "index" : "38" });
                    skLineSegment(sketch, "tTOCUaFlARpN", { "construction" : false, "index" : "39" });
                    skLineSegment(sketch, "D9r2EV7pIqlj.bottom", { "construction" : false, "index" : "41" });
                    skLineSegment(sketch, "D9r2EV7pIqlj.top", { "construction" : false, "index" : "42" });
                    skLineSegment(sketch, "D9r2EV7pIqlj.left", { "construction" : false, "index" : "43" });
                    skLineSegment(sketch, "D9r2EV7pIqlj.right", { "construction" : false, "index" : "44" });
                    skPoint(sketch, "WxDR8351T9aN.top.start.orphan", { "construction" : false, "index" : "9" });
                    skPoint(sketch, "WxDR8351T9aN.bottom.start.orphan", { "construction" : false, "index" : "10" });
                    skLineSegment(sketch, "jgc1xXvtFeQw", { "construction" : true, "index" : "45" });
                    skLineSegment(sketch, "Cxk5xPmxKsRz.bottom", { "construction" : false, "index" : "46" });
                    skLineSegment(sketch, "Cxk5xPmxKsRz.top", { "construction" : false, "index" : "47" });
                    skLineSegment(sketch, "Cxk5xPmxKsRz.left", { "construction" : false, "index" : "48" });
                    skLineSegment(sketch, "Cxk5xPmxKsRz.right", { "construction" : false, "index" : "49" });
                    skPoint(sketch, "Cxk5xPmxKsRz.middle", { "construction" : true, "index" : "11" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.0.1.0", { "construction" : false, "index" : "50" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.1.1.0", { "construction" : false, "index" : "51" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.2.1.0", { "construction" : false, "index" : "52" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.3.1.0", { "construction" : false, "index" : "53" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.4.1.0", { "construction" : false, "index" : "54" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.5.1.0", { "construction" : false, "index" : "55" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.6.1.0", { "construction" : false, "index" : "56" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.7.1.0", { "construction" : false, "index" : "57" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.direction1", { "construction" : true, "index" : "58" });
                    skLineSegment(sketch, "1h5lzhu1DqW3.bottom", { "construction" : false, "index" : "59" });
                    skLineSegment(sketch, "1h5lzhu1DqW3.top", { "construction" : false, "index" : "60" });
                    skLineSegment(sketch, "1h5lzhu1DqW3.left", { "construction" : false, "index" : "61" });
                    skLineSegment(sketch, "1h5lzhu1DqW3.right", { "construction" : false, "index" : "62" });
                    skLineSegment(sketch, "tgSFU2ntIwoe", { "construction" : true, "index" : "63" });
                    skLineSegment(sketch, "vNZaYuuZX5WG.bottom", { "construction" : false, "index" : "64" });
                    skLineSegment(sketch, "vNZaYuuZX5WG.top", { "construction" : false, "index" : "65" });
                    skLineSegment(sketch, "vNZaYuuZX5WG.left", { "construction" : false, "index" : "66" });
                    skLineSegment(sketch, "vNZaYuuZX5WG.right", { "construction" : false, "index" : "67" });
                    skPoint(sketch, "vNZaYuuZX5WG.middle", { "construction" : true, "index" : "12" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.0.1.0", { "construction" : false, "index" : "68" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.1.1.0", { "construction" : false, "index" : "69" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.2.1.0", { "construction" : false, "index" : "70" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.3.1.0", { "construction" : false, "index" : "71" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.4.1.0", { "construction" : false, "index" : "72" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.5.1.0", { "construction" : false, "index" : "73" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.6.1.0", { "construction" : false, "index" : "74" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.7.1.0", { "construction" : false, "index" : "75" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.direction1", { "construction" : true, "index" : "84" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.3.2.0", { "construction" : false, "index" : "85" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.12.2.0", { "construction" : false, "index" : "86" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.18.2.0", { "construction" : false, "index" : "87" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.9.2.0", { "construction" : false, "index" : "88" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.12.2.0", { "construction" : false, "index" : "89" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.0.2.0", { "construction" : false, "index" : "90" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.3.2.0", { "construction" : false, "index" : "91" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.6.2.0", { "construction" : false, "index" : "92" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.9.2.0", { "construction" : false, "index" : "93" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.15.2.0", { "construction" : false, "index" : "94" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.0.2.0", { "construction" : false, "index" : "95" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.6.2.0", { "construction" : false, "index" : "96" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.15.2.0", { "construction" : false, "index" : "97" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.21.2.0", { "construction" : false, "index" : "98" });
                    skLineSegment(sketch, "gAMrxUh9DdFH.18.2.0", { "construction" : false, "index" : "99" });
                    skLineSegment(sketch, "W5o7WVF6PNS6.21.2.0", { "construction" : false, "index" : "100" });
                    skLineSegment(sketch, "ilE1DLjavjuI", { "construction" : false, "index" : "101" });
                    {
                        var TGUPdMLLyhzIqG_query;
                        TGUPdMLLyhzIqG_query=qCompressed(1.0,"%B5$QueryM4Sa$entityTypeBa$EntityTypeS6$VERTEXSb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S6.7$OriginpointOpS9$queryTypeS5$DUMMY",id);
                        skConstraint(sketch, "dZ52UiFd4O3Z.middle.positionSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "5", "name" : "", "localFirst" : "dZ52UiFd4O3Z.middle", "externalSecond" : qUnion([TGUPdMLLyhzIqG_query]) });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.mid1", { "constraintType" : ConstraintType.MIDPOINT, "index" : "3", "name" : "", "localMidpoint" : "icpN5dRRYMN3.middle", "localEntity1" : "icpN5dRRYMN3.top.start", "localEntity2" : "icpN5dRRYMN3.bottom.end" });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.mid2", { "constraintType" : ConstraintType.MIDPOINT, "index" : "4", "name" : "", "localMidpoint" : "icpN5dRRYMN3.middle", "localEntity1" : "icpN5dRRYMN3.top.end", "localEntity2" : "icpN5dRRYMN3.bottom.start" });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.perpendicular", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "2", "name" : "", "localFirst" : "icpN5dRRYMN3.top", "localSecond" : "icpN5dRRYMN3.left" });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.parallel.1", { "constraintType" : ConstraintType.PARALLEL, "index" : "3", "name" : "", "localFirst" : "icpN5dRRYMN3.bottom", "localSecond" : "icpN5dRRYMN3.top" });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.parallel.2", { "constraintType" : ConstraintType.PARALLEL, "index" : "4", "name" : "", "localFirst" : "icpN5dRRYMN3.left", "localSecond" : "icpN5dRRYMN3.right" });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.horizontal", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "2", "name" : "", "localFirst" : "icpN5dRRYMN3.top" });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.corner0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "6", "name" : "", "localFirst" : "icpN5dRRYMN3.bottom.start", "localSecond" : "icpN5dRRYMN3.left.start" });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.corner1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "7", "name" : "", "localFirst" : "icpN5dRRYMN3.bottom.end", "localSecond" : "icpN5dRRYMN3.right.start" });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.corner2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "8", "name" : "", "localFirst" : "icpN5dRRYMN3.top.start", "localSecond" : "icpN5dRRYMN3.left.end" });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.corner3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "9", "name" : "", "localFirst" : "icpN5dRRYMN3.top.end", "localSecond" : "icpN5dRRYMN3.right.end" });
                    }
                    {
                        skConstraint(sketch, "icpN5dRRYMN3.middle.positionSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "10", "name" : "", "localFirst" : "icpN5dRRYMN3.middle", "localSecond" : "dZ52UiFd4O3Z.middle" });
                    }
                    {
                        skConstraint(sketch, "aLFbDaUhhSjB", { "constraintType" : ConstraintType.DISTANCE, "index" : "3", "name" : "", "localFirst" : "icpN5dRRYMN3.left", "localSecond" : "icpN5dRRYMN3.right", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('sp_width') + (lookup('sp_tolerance') * 2)), 'expression' : "#sp_width + (#sp_tolerance*2)" }.value, "halfSpace0" : DimensionHalfSpace.LEFT, "halfSpace1" : DimensionHalfSpace.RIGHT, "labelRatio" : 0.5124839046146816, "labelDistance" : -0.026210085145895082 * meter });
                    }
                    {
                        skConstraint(sketch, "mntGTOlR2Asn", { "constraintType" : ConstraintType.DISTANCE, "index" : "4", "name" : "", "localFirst" : "icpN5dRRYMN3.top", "localSecond" : "icpN5dRRYMN3.bottom", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('sp_height') + (lookup('sp_tolerance') * 2)), 'expression' : "#sp_height + (#sp_tolerance * 2)" }.value, "halfSpace0" : DimensionHalfSpace.LEFT, "halfSpace1" : DimensionHalfSpace.RIGHT, "labelRatio" : 0.5, "labelDistance" : 0.03944856487425284 * meter });
                    }
                    {
                        skConstraint(sketch, "AGiFvdYnC1Gj.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "19", "name" : "", "localFirst" : "AGiFvdYnC1Gj.start", "localSecond" : "icpN5dRRYMN3.bottom.start" });
                    }
                    {
                        skConstraint(sketch, "GuM1oInfJLRW.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "21", "name" : "", "localFirst" : "GuM1oInfJLRW.start", "localSecond" : "icpN5dRRYMN3.bottom.end" });
                    }
                    {
                        skConstraint(sketch, "GuM1oInfJLRW.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "1", "name" : "", "localFirst" : "GuM1oInfJLRW" });
                    }
                    {
                        skConstraint(sketch, "CKUJZR0utnep.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "33", "name" : "", "localFirst" : "CKUJZR0utnep.start", "localSecond" : "icpN5dRRYMN3.bottom.end" });
                    }
                    {
                        skConstraint(sketch, "CKUJZR0utnep.endSnap0", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "8", "name" : "", "localFirst" : "CKUJZR0utnep", "localSecond" : "icpN5dRRYMN3.right" });
                    }
                    {
                        skConstraint(sketch, "YsesDGRtfLOg", { "constraintType" : ConstraintType.LENGTH, "index" : "1", "name" : "", "localFirst" : "CKUJZR0utnep", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('bracket_rail_width') + lookup('bracket_rail_tolerance')), 'expression' : "#bracket_rail_width + #bracket_rail_tolerance" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.4999999999999982, "labelDistance" : -0.0019247075422648557 * meter });
                    }
                    {
                        skConstraint(sketch, "ggliDtHZBie1.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "34", "name" : "", "localFirst" : "ggliDtHZBie1.start", "localSecond" : "icpN5dRRYMN3.bottom.start" });
                    }
                    {
                        skConstraint(sketch, "ggliDtHZBie1.endSnap0", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "9", "name" : "", "localFirst" : "ggliDtHZBie1", "localSecond" : "icpN5dRRYMN3.left" });
                    }
                    {
                        skConstraint(sketch, "R1LO0huzIbP8", { "constraintType" : ConstraintType.LENGTH, "index" : "2", "name" : "", "localFirst" : "ggliDtHZBie1", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('bracket_rail_width') + lookup('bracket_rail_tolerance')), 'expression' : "#bracket_rail_width + #bracket_rail_tolerance" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.4999999999999982, "labelDistance" : -0.0020391566084645985 * meter });
                    }
                    {
                        skConstraint(sketch, "8iArdl5WzThi.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "35", "name" : "", "localFirst" : "8iArdl5WzThi.start", "localSecond" : "CKUJZR0utnep.end" });
                    }
                    {
                        skConstraint(sketch, "8iArdl5WzThi.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "2", "name" : "", "localFirst" : "8iArdl5WzThi" });
                    }
                    {
                        skConstraint(sketch, "ZW8XGf4wY7W4.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "37", "name" : "", "localFirst" : "ZW8XGf4wY7W4.start", "localSecond" : "ggliDtHZBie1.end" });
                    }
                    {
                        skConstraint(sketch, "WE6aMD0pgltb.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "39", "name" : "", "localFirst" : "WE6aMD0pgltb.start", "localSecond" : "icpN5dRRYMN3.top.start" });
                    }
                    {
                        skConstraint(sketch, "WE6aMD0pgltb.endSnap0", { "constraintType" : ConstraintType.PARALLEL, "index" : "13", "name" : "", "localFirst" : "WE6aMD0pgltb", "localSecond" : "icpN5dRRYMN3.left" });
                    }
                    {
                        skConstraint(sketch, "GQzM1rs8KZsm.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "41", "name" : "", "localFirst" : "GQzM1rs8KZsm.start", "localSecond" : "icpN5dRRYMN3.top.end" });
                    }
                    {
                        skConstraint(sketch, "GQzM1rs8KZsm.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "3", "name" : "", "localFirst" : "GQzM1rs8KZsm" });
                    }
                    {
                        skConstraint(sketch, "GVVj7fIeszJ7", { "constraintType" : ConstraintType.DISTANCE, "index" : "13", "name" : "", "localFirst" : "icpN5dRRYMN3.left", "localSecond" : "dZ52UiFd4O3Z.left", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('asm_left_switch_w_total')), 'expression' : "#asm_left_switch_w_total" }.value, "halfSpace0" : DimensionHalfSpace.RIGHT, "halfSpace1" : DimensionHalfSpace.LEFT, "labelRatio" : 0.5193999670148618, "labelDistance" : -0.04196834054459168 * meter });
                    }
                    {
                        skConstraint(sketch, "R4fthY4SaoUb.startSnap0", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "7", "name" : "", "localFirst" : "R4fthY4SaoUb.start", "localSecond" : "8iArdl5WzThi.end" });
                    }
                    {
                        skConstraint(sketch, "R4fthY4SaoUb.endSnap0", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "8", "name" : "", "localFirst" : "R4fthY4SaoUb.end", "localSecond" : "GQzM1rs8KZsm.end" });
                    }
                    {
                        skConstraint(sketch, "R4fthY4SaoUb.endSnap1", { "constraintType" : ConstraintType.VERTICAL, "index" : "4", "name" : "", "localFirst" : "R4fthY4SaoUb" });
                    }
                    {
                        skConstraint(sketch, "RxqUTiUH2la0", { "constraintType" : ConstraintType.DISTANCE, "index" : "14", "name" : "", "localFirst" : "icpN5dRRYMN3.right", "localSecond" : "R4fthY4SaoUb", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('asm_right_switch_w_total')), 'expression' : "#asm_right_switch_w_total" }.value, "halfSpace0" : DimensionHalfSpace.LEFT, "halfSpace1" : DimensionHalfSpace.RIGHT, "labelRatio" : 0.45221488794865133, "labelDistance" : -0.04120977222919464 * meter });
                    }
                    {
                        skConstraint(sketch, "E5psPBdazMi6.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "42", "name" : "", "localFirst" : "E5psPBdazMi6.start", "localSecond" : "R4fthY4SaoUb.end" });
                    }
                    {
                        skConstraint(sketch, "E5psPBdazMi6.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "5", "name" : "", "localFirst" : "E5psPBdazMi6.end", "localSecond" : "dZ52UiFd4O3Z.left.end" });
                    }
                    {
                        skConstraint(sketch, "E5psPBdazMi6.endSnap1", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "10", "name" : "", "localFirst" : "E5psPBdazMi6", "localSecond" : "R4fthY4SaoUb" });
                    }
                    {
                        skConstraint(sketch, "qi2Ta53CvWH1.startSnap", { "constraintType" : ConstraintType.COINCIDENT, "index" : "43", "name" : "", "localFirst" : "qi2Ta53CvWH1.start", "localSecond" : "E5psPBdazMi6.end" });
                    }
                    {
                        skConstraint(sketch, "qi2Ta53CvWH1.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "44", "name" : "", "localFirst" : "qi2Ta53CvWH1.end", "localSecond" : "dZ52UiFd4O3Z.left.end" });
                    }
                    {
                        skConstraint(sketch, "EFh1IGCAhwQi.startSnap0", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "9", "name" : "", "localFirst" : "EFh1IGCAhwQi.start", "localSecond" : "R4fthY4SaoUb.start" });
                    }
                    {
                        skConstraint(sketch, "EFh1IGCAhwQi.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "6", "name" : "", "localFirst" : "EFh1IGCAhwQi.end", "localSecond" : "dZ52UiFd4O3Z.left.start" });
                    }
                    {
                        skConstraint(sketch, "EFh1IGCAhwQi.endSnap1", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "11", "name" : "", "localFirst" : "EFh1IGCAhwQi", "localSecond" : "dZ52UiFd4O3Z.left" });
                    }
                    {
                        skConstraint(sketch, "tTOCUaFlARpN.startSnap", { "constraintType" : ConstraintType.COINCIDENT, "index" : "45", "name" : "", "localFirst" : "tTOCUaFlARpN.start", "localSecond" : "EFh1IGCAhwQi.end" });
                    }
                    {
                        skConstraint(sketch, "tTOCUaFlARpN.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "46", "name" : "", "localFirst" : "tTOCUaFlARpN.end", "localSecond" : "dZ52UiFd4O3Z.left.start" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.firstSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "48", "name" : "", "localFirst" : "D9r2EV7pIqlj.bottom.start", "localSecond" : "icpN5dRRYMN3.right" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.firstSnap1", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "10", "name" : "", "localFirst" : "D9r2EV7pIqlj.bottom.start", "localSecond" : "WxDR8351T9aN.bottom.start.orphan" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.oppositeSnap0", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "11", "name" : "", "localFirst" : "D9r2EV7pIqlj.top.end", "localSecond" : "WxDR8351T9aN.top.start.orphan" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.perpendicular", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "13", "name" : "", "localFirst" : "D9r2EV7pIqlj.top", "localSecond" : "D9r2EV7pIqlj.left" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.parallel.1", { "constraintType" : ConstraintType.PARALLEL, "index" : "14", "name" : "", "localFirst" : "D9r2EV7pIqlj.bottom", "localSecond" : "D9r2EV7pIqlj.top" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.parallel.2", { "constraintType" : ConstraintType.PARALLEL, "index" : "15", "name" : "", "localFirst" : "D9r2EV7pIqlj.left", "localSecond" : "D9r2EV7pIqlj.right" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.horizontal", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "12", "name" : "", "localFirst" : "D9r2EV7pIqlj.top" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.corner0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "49", "name" : "", "localFirst" : "D9r2EV7pIqlj.bottom.start", "localSecond" : "D9r2EV7pIqlj.left.start" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.corner1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "50", "name" : "", "localFirst" : "D9r2EV7pIqlj.bottom.end", "localSecond" : "D9r2EV7pIqlj.right.start" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.corner2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "51", "name" : "", "localFirst" : "D9r2EV7pIqlj.top.start", "localSecond" : "D9r2EV7pIqlj.left.end" });
                    }
                    {
                        skConstraint(sketch, "D9r2EV7pIqlj.corner3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "52", "name" : "", "localFirst" : "D9r2EV7pIqlj.top.end", "localSecond" : "D9r2EV7pIqlj.right.end" });
                    }
                    {
                        skConstraint(sketch, "7yu2KmVvzGqd", { "constraintType" : ConstraintType.DISTANCE, "index" : "15", "name" : "", "localFirst" : "D9r2EV7pIqlj.left", "localSecond" : "D9r2EV7pIqlj.right", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('switch_width') + (lookup('switch_tolerance') * 2)), 'expression' : "#switch_width + (#switch_tolerance * 2)" }.value, "halfSpace0" : DimensionHalfSpace.RIGHT, "halfSpace1" : DimensionHalfSpace.LEFT, "labelRatio" : 0.5, "labelDistance" : 0.037343371811197616 * meter });
                    }
                    {
                        skConstraint(sketch, "R9GqYp6G1Pt5", { "constraintType" : ConstraintType.DISTANCE, "index" : "16", "name" : "", "localFirst" : "D9r2EV7pIqlj.top", "localSecond" : "D9r2EV7pIqlj.bottom", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('switch_height') + (lookup('switch_tolerance') * 2)), 'expression' : "#switch_height + (#switch_tolerance * 2)" }.value, "halfSpace0" : DimensionHalfSpace.RIGHT, "halfSpace1" : DimensionHalfSpace.LEFT, "labelRatio" : 0.5, "labelDistance" : -0.012851785726587975 * meter });
                    }
                    {
                        skConstraint(sketch, "jgc1xXvtFeQw.startSnap0", { "constraintType" : ConstraintType.MIDPOINT, "index" : "8", "name" : "", "localEntity1" : "jgc1xXvtFeQw.start", "localEntity2" : "D9r2EV7pIqlj.bottom" });
                    }
                    {
                        skConstraint(sketch, "jgc1xXvtFeQw.endSnap0", { "constraintType" : ConstraintType.MIDPOINT, "index" : "9", "name" : "", "localEntity1" : "jgc1xXvtFeQw.end", "localEntity2" : "D9r2EV7pIqlj.top" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.mid1", { "constraintType" : ConstraintType.MIDPOINT, "index" : "10", "name" : "", "localMidpoint" : "Cxk5xPmxKsRz.middle", "localEntity1" : "Cxk5xPmxKsRz.top.start", "localEntity2" : "Cxk5xPmxKsRz.bottom.end" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.mid2", { "constraintType" : ConstraintType.MIDPOINT, "index" : "11", "name" : "", "localMidpoint" : "Cxk5xPmxKsRz.middle", "localEntity1" : "Cxk5xPmxKsRz.top.end", "localEntity2" : "Cxk5xPmxKsRz.bottom.start" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.perpendicular", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "14", "name" : "", "localFirst" : "Cxk5xPmxKsRz.top", "localSecond" : "Cxk5xPmxKsRz.left" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.parallel.1", { "constraintType" : ConstraintType.PARALLEL, "index" : "16", "name" : "", "localFirst" : "Cxk5xPmxKsRz.bottom", "localSecond" : "Cxk5xPmxKsRz.top" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.parallel.2", { "constraintType" : ConstraintType.PARALLEL, "index" : "17", "name" : "", "localFirst" : "Cxk5xPmxKsRz.left", "localSecond" : "Cxk5xPmxKsRz.right" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.horizontal", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "13", "name" : "", "localFirst" : "Cxk5xPmxKsRz.top" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.corner0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "53", "name" : "", "localFirst" : "Cxk5xPmxKsRz.bottom.start", "localSecond" : "Cxk5xPmxKsRz.left.start" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.corner1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "54", "name" : "", "localFirst" : "Cxk5xPmxKsRz.bottom.end", "localSecond" : "Cxk5xPmxKsRz.right.start" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.corner2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "55", "name" : "", "localFirst" : "Cxk5xPmxKsRz.top.start", "localSecond" : "Cxk5xPmxKsRz.left.end" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.corner3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "56", "name" : "", "localFirst" : "Cxk5xPmxKsRz.top.end", "localSecond" : "Cxk5xPmxKsRz.right.end" });
                    }
                    {
                        skConstraint(sketch, "Cxk5xPmxKsRz.middle.positionSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "57", "name" : "", "localFirst" : "Cxk5xPmxKsRz.middle", "localSecond" : "jgc1xXvtFeQw" });
                    }
                    {
                        var mSHIAwkPoBnGHr_query;
                        mSHIAwkPoBnGHr_query=qCompressed(1.0,"%B5$QueryM4Sa$entityTypeBa$EntityTypeS6$VERTEXSb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S6.7$OriginpointOpS9$queryTypeS5$DUMMY",id);
                        skConstraint(sketch, "Cxk5xPmxKsRz.middle.positionSnap1", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "14", "name" : "", "localFirst" : "Cxk5xPmxKsRz.middle", "externalSecond" : qUnion([mSHIAwkPoBnGHr_query]) });
                    }
                    {
                        skConstraint(sketch, "pXIAlWnTT8hW", { "constraintType" : ConstraintType.DISTANCE, "index" : "17", "name" : "", "localFirst" : "Cxk5xPmxKsRz.left", "localSecond" : "Cxk5xPmxKsRz.right", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('mag_width') + (lookup('mag_xy_tolerance') * 2)), 'expression' : "#mag_width+ (#mag_xy_tolerance *2)" }.value, "halfSpace0" : DimensionHalfSpace.LEFT, "halfSpace1" : DimensionHalfSpace.RIGHT, "labelRatio" : 0.5, "labelDistance" : 0.02007100090551221 * meter });
                    }
                    {
                        skConstraint(sketch, "ggO6fmWntJfy", { "constraintType" : ConstraintType.DISTANCE, "index" : "18", "name" : "", "localFirst" : "Cxk5xPmxKsRz.top", "localSecond" : "Cxk5xPmxKsRz.bottom", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('mag_height') + (lookup('mag_xy_tolerance') * 2)), 'expression' : "#mag_height+ (#mag_xy_tolerance *2)" }.value, "halfSpace0" : DimensionHalfSpace.LEFT, "halfSpace1" : DimensionHalfSpace.RIGHT, "labelRatio" : 0.5, "labelDistance" : -0.010679527100193844 * meter });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.24.1.0.coi0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "58", "name" : "", "localFirst" : "W5o7WVF6PNS6.0.1.0.start", "localSecond" : "W5o7WVF6PNS6.7.1.0.start", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.24.1.0.coi1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "59", "name" : "", "localFirst" : "W5o7WVF6PNS6.0.1.0.end", "localSecond" : "W5o7WVF6PNS6.1.1.0.start", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.24.1.0.coi2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "60", "name" : "", "localFirst" : "W5o7WVF6PNS6.2.1.0.start", "localSecond" : "W5o7WVF6PNS6.7.1.0.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.24.1.0.coi3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "61", "name" : "", "localFirst" : "W5o7WVF6PNS6.2.1.0.end", "localSecond" : "W5o7WVF6PNS6.1.1.0.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.24.1.0.coi4", { "constraintType" : ConstraintType.COINCIDENT, "index" : "62", "name" : "", "localFirst" : "W5o7WVF6PNS6.3.1.0.start", "localSecond" : "W5o7WVF6PNS6.5.1.0.start", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.24.1.0.coi5", { "constraintType" : ConstraintType.COINCIDENT, "index" : "63", "name" : "", "localFirst" : "W5o7WVF6PNS6.3.1.0.end", "localSecond" : "W5o7WVF6PNS6.4.1.0.start", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.24.1.0.coi6", { "constraintType" : ConstraintType.COINCIDENT, "index" : "64", "name" : "", "localFirst" : "W5o7WVF6PNS6.6.1.0.start", "localSecond" : "W5o7WVF6PNS6.5.1.0.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.24.1.0.coi7", { "constraintType" : ConstraintType.COINCIDENT, "index" : "65", "name" : "", "localFirst" : "W5o7WVF6PNS6.6.1.0.end", "localSecond" : "W5o7WVF6PNS6.4.1.0.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.hv1", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "15", "name" : "", "localFirst" : "W5o7WVF6PNS6.direction1", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.len1.c1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "66", "name" : "", "localFirst" : "W5o7WVF6PNS6.direction1.start", "localSecond" : "D9r2EV7pIqlj.right.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.len1.c2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "67", "name" : "", "localFirst" : "W5o7WVF6PNS6.direction1.end", "localSecond" : "W5o7WVF6PNS6.1.1.0.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.len1.length", { "constraintType" : ConstraintType.LENGTH, "index" : "4", "name" : "", "localFirst" : "W5o7WVF6PNS6.direction1", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('switch_width') + (lookup('switch_tolerance') * 2) + lookup('switch_pitch')), 'expression' : "#switch_width + (#switch_tolerance * 2) + #switch_pitch" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.5000000000000001, "labelDistance" : 0.009705902482182317 * meter, "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.pattern", { "constraintType" : ConstraintType.LINEAR_PATTERN, "index" : "1", "name" : "", "patterng" : { 'value' : try(roundWithinTolerance(24)), 'expression' : "" }.value, "patternc1" : { 'value' : try(roundWithinTolerance(lookup('num_switches_right') + 1)), 'expression' : "#num_switches_right + 1" }.value, "previouspatternc1" : { 'value' : try(roundWithinTolerance(3)), 'expression' : "" }.value, "maximumpatterng" : { 'value' : try(roundWithinTolerance(24)), 'expression' : "" }.value, "patternc2" : { 'value' : try(roundWithinTolerance(1)), 'expression' : "" }.value, "previouspatternc2" : { 'value' : try(roundWithinTolerance(1)), 'expression' : "" }.value, "localInstance0,0,0" : "D9r2EV7pIqlj.bottom", "localInstance1,0,0" : "D9r2EV7pIqlj.bottom.start", "localInstance2,0,0" : "D9r2EV7pIqlj.bottom.end", "localInstance3,0,0" : "D9r2EV7pIqlj.right", "localInstance4,0,0" : "D9r2EV7pIqlj.right.start", "localInstance5,0,0" : "D9r2EV7pIqlj.right.end", "localInstance6,0,0" : "D9r2EV7pIqlj.top", "localInstance7,0,0" : "D9r2EV7pIqlj.top.start", "localInstance8,0,0" : "D9r2EV7pIqlj.top.end", "localInstance9,0,0" : "Cxk5xPmxKsRz.bottom", "localInstance10,0,0" : "Cxk5xPmxKsRz.bottom.start", "localInstance11,0,0" : "Cxk5xPmxKsRz.bottom.end", "localInstance12,0,0" : "Cxk5xPmxKsRz.right", "localInstance13,0,0" : "Cxk5xPmxKsRz.right.start", "localInstance14,0,0" : "Cxk5xPmxKsRz.right.end", "localInstance15,0,0" : "Cxk5xPmxKsRz.left", "localInstance16,0,0" : "Cxk5xPmxKsRz.left.start", "localInstance17,0,0" : "Cxk5xPmxKsRz.left.end", "localInstance18,0,0" : "Cxk5xPmxKsRz.top", "localInstance19,0,0" : "Cxk5xPmxKsRz.top.start", "localInstance20,0,0" : "Cxk5xPmxKsRz.top.end", "localInstance21,0,0" : "D9r2EV7pIqlj.left", "localInstance22,0,0" : "D9r2EV7pIqlj.left.start", "localInstance23,0,0" : "D9r2EV7pIqlj.left.end", "localInstance0,1,0" : "W5o7WVF6PNS6.0.1.0", "localInstance1,1,0" : "W5o7WVF6PNS6.0.1.0.start", "localInstance2,1,0" : "W5o7WVF6PNS6.0.1.0.end", "localInstance3,1,0" : "W5o7WVF6PNS6.1.1.0", "localInstance4,1,0" : "W5o7WVF6PNS6.1.1.0.start", "localInstance5,1,0" : "W5o7WVF6PNS6.1.1.0.end", "localInstance6,1,0" : "W5o7WVF6PNS6.2.1.0", "localInstance7,1,0" : "W5o7WVF6PNS6.2.1.0.start", "localInstance8,1,0" : "W5o7WVF6PNS6.2.1.0.end", "localInstance9,1,0" : "W5o7WVF6PNS6.3.1.0", "localInstance10,1,0" : "W5o7WVF6PNS6.3.1.0.start", "localInstance11,1,0" : "W5o7WVF6PNS6.3.1.0.end", "localInstance12,1,0" : "W5o7WVF6PNS6.4.1.0", "localInstance13,1,0" : "W5o7WVF6PNS6.4.1.0.start", "localInstance14,1,0" : "W5o7WVF6PNS6.4.1.0.end", "localInstance15,1,0" : "W5o7WVF6PNS6.5.1.0", "localInstance16,1,0" : "W5o7WVF6PNS6.5.1.0.start", "localInstance17,1,0" : "W5o7WVF6PNS6.5.1.0.end", "localInstance18,1,0" : "W5o7WVF6PNS6.6.1.0", "localInstance19,1,0" : "W5o7WVF6PNS6.6.1.0.start", "localInstance20,1,0" : "W5o7WVF6PNS6.6.1.0.end", "localInstance21,1,0" : "W5o7WVF6PNS6.7.1.0", "localInstance22,1,0" : "W5o7WVF6PNS6.7.1.0.start", "localInstance23,1,0" : "W5o7WVF6PNS6.7.1.0.end", "localDirection1" : "W5o7WVF6PNS6.direction1", "labelDistance" : 0.01372621892537292 * meter, "labelAngle" : -0.7853981633974483 * radian, "sketchToolType" : SketchToolType.PATTERN, "localInstance0,2,0" : "W5o7WVF6PNS6.0.2.0", "localInstance3,2,0" : "W5o7WVF6PNS6.3.2.0", "localInstance6,2,0" : "W5o7WVF6PNS6.6.2.0", "localInstance9,2,0" : "W5o7WVF6PNS6.9.2.0", "localInstance12,2,0" : "W5o7WVF6PNS6.12.2.0", "localInstance15,2,0" : "W5o7WVF6PNS6.15.2.0", "localInstance18,2,0" : "W5o7WVF6PNS6.18.2.0", "localInstance21,2,0" : "W5o7WVF6PNS6.21.2.0", "localInstance1,2,0" : "W5o7WVF6PNS6.0.2.0.start", "localInstance2,2,0" : "W5o7WVF6PNS6.0.2.0.end", "localInstance4,2,0" : "W5o7WVF6PNS6.3.2.0.start", "localInstance5,2,0" : "W5o7WVF6PNS6.3.2.0.end", "localInstance7,2,0" : "W5o7WVF6PNS6.6.2.0.start", "localInstance8,2,0" : "W5o7WVF6PNS6.6.2.0.end", "localInstance10,2,0" : "W5o7WVF6PNS6.9.2.0.start", "localInstance11,2,0" : "W5o7WVF6PNS6.9.2.0.end", "localInstance13,2,0" : "W5o7WVF6PNS6.12.2.0.start", "localInstance14,2,0" : "W5o7WVF6PNS6.12.2.0.end", "localInstance16,2,0" : "W5o7WVF6PNS6.15.2.0.start", "localInstance17,2,0" : "W5o7WVF6PNS6.15.2.0.end", "localInstance19,2,0" : "W5o7WVF6PNS6.18.2.0.start", "localInstance20,2,0" : "W5o7WVF6PNS6.18.2.0.end", "localInstance22,2,0" : "W5o7WVF6PNS6.21.2.0.start", "localInstance23,2,0" : "W5o7WVF6PNS6.21.2.0.end" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.firstSnap0", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "16", "name" : "", "localFirst" : "1h5lzhu1DqW3.bottom.start", "localSecond" : "D9r2EV7pIqlj.bottom.end" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.firstSnap1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "68", "name" : "", "localFirst" : "1h5lzhu1DqW3.bottom.start", "localSecond" : "icpN5dRRYMN3.left" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.oppositeSnap0", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "17", "name" : "", "localFirst" : "1h5lzhu1DqW3.top.end", "localSecond" : "D9r2EV7pIqlj.top.end" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.perpendicular", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "15", "name" : "", "localFirst" : "1h5lzhu1DqW3.top", "localSecond" : "1h5lzhu1DqW3.left" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.parallel.1", { "constraintType" : ConstraintType.PARALLEL, "index" : "18", "name" : "", "localFirst" : "1h5lzhu1DqW3.bottom", "localSecond" : "1h5lzhu1DqW3.top" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.parallel.2", { "constraintType" : ConstraintType.PARALLEL, "index" : "19", "name" : "", "localFirst" : "1h5lzhu1DqW3.left", "localSecond" : "1h5lzhu1DqW3.right" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.horizontal", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "18", "name" : "", "localFirst" : "1h5lzhu1DqW3.top" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.corner0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "69", "name" : "", "localFirst" : "1h5lzhu1DqW3.bottom.start", "localSecond" : "1h5lzhu1DqW3.left.start" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.corner1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "70", "name" : "", "localFirst" : "1h5lzhu1DqW3.bottom.end", "localSecond" : "1h5lzhu1DqW3.right.start" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.corner2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "71", "name" : "", "localFirst" : "1h5lzhu1DqW3.top.start", "localSecond" : "1h5lzhu1DqW3.left.end" });
                    }
                    {
                        skConstraint(sketch, "1h5lzhu1DqW3.corner3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "72", "name" : "", "localFirst" : "1h5lzhu1DqW3.top.end", "localSecond" : "1h5lzhu1DqW3.right.end" });
                    }
                    {
                        skConstraint(sketch, "cG6z0XR1NDq9", { "constraintType" : ConstraintType.DISTANCE, "index" : "19", "name" : "", "localFirst" : "1h5lzhu1DqW3.left", "localSecond" : "1h5lzhu1DqW3.right", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('switch_width') + (lookup('switch_tolerance') * 2)), 'expression' : "#switch_width + (#switch_tolerance * 2)" }.value, "halfSpace0" : DimensionHalfSpace.LEFT, "halfSpace1" : DimensionHalfSpace.RIGHT, "labelRatio" : 0.5, "labelDistance" : 0.0373918995289246 * meter });
                    }
                    {
                        skConstraint(sketch, "tgSFU2ntIwoe.startSnap0", { "constraintType" : ConstraintType.MIDPOINT, "index" : "12", "name" : "", "localEntity1" : "tgSFU2ntIwoe.start", "localEntity2" : "1h5lzhu1DqW3.bottom" });
                    }
                    {
                        skConstraint(sketch, "tgSFU2ntIwoe.endSnap0", { "constraintType" : ConstraintType.MIDPOINT, "index" : "13", "name" : "", "localEntity1" : "tgSFU2ntIwoe.end", "localEntity2" : "1h5lzhu1DqW3.top" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.mid1", { "constraintType" : ConstraintType.MIDPOINT, "index" : "14", "name" : "", "localMidpoint" : "vNZaYuuZX5WG.middle", "localEntity1" : "vNZaYuuZX5WG.top.start", "localEntity2" : "vNZaYuuZX5WG.bottom.end" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.mid2", { "constraintType" : ConstraintType.MIDPOINT, "index" : "15", "name" : "", "localMidpoint" : "vNZaYuuZX5WG.middle", "localEntity1" : "vNZaYuuZX5WG.top.end", "localEntity2" : "vNZaYuuZX5WG.bottom.start" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.perpendicular", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "16", "name" : "", "localFirst" : "vNZaYuuZX5WG.top", "localSecond" : "vNZaYuuZX5WG.left" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.parallel.1", { "constraintType" : ConstraintType.PARALLEL, "index" : "20", "name" : "", "localFirst" : "vNZaYuuZX5WG.bottom", "localSecond" : "vNZaYuuZX5WG.top" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.parallel.2", { "constraintType" : ConstraintType.PARALLEL, "index" : "21", "name" : "", "localFirst" : "vNZaYuuZX5WG.left", "localSecond" : "vNZaYuuZX5WG.right" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.horizontal", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "19", "name" : "", "localFirst" : "vNZaYuuZX5WG.top" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.corner0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "73", "name" : "", "localFirst" : "vNZaYuuZX5WG.bottom.start", "localSecond" : "vNZaYuuZX5WG.left.start" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.corner1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "74", "name" : "", "localFirst" : "vNZaYuuZX5WG.bottom.end", "localSecond" : "vNZaYuuZX5WG.right.start" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.corner2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "75", "name" : "", "localFirst" : "vNZaYuuZX5WG.top.start", "localSecond" : "vNZaYuuZX5WG.left.end" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.corner3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "76", "name" : "", "localFirst" : "vNZaYuuZX5WG.top.end", "localSecond" : "vNZaYuuZX5WG.right.end" });
                    }
                    {
                        skConstraint(sketch, "vNZaYuuZX5WG.middle.positionSnap0", { "constraintType" : ConstraintType.MIDPOINT, "index" : "16", "name" : "", "localEntity1" : "vNZaYuuZX5WG.middle", "localEntity2" : "tgSFU2ntIwoe" });
                    }
                    {
                        skConstraint(sketch, "sx40X3yqW4uw", { "constraintType" : ConstraintType.DISTANCE, "index" : "20", "name" : "", "localFirst" : "vNZaYuuZX5WG.top", "localSecond" : "vNZaYuuZX5WG.bottom", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('mag_height') + (lookup('mag_xy_tolerance') * 2)), 'expression' : "#mag_height+ (#mag_xy_tolerance *2)" }.value, "halfSpace0" : DimensionHalfSpace.RIGHT, "halfSpace1" : DimensionHalfSpace.LEFT, "labelRatio" : 0.5, "labelDistance" : -0.012661421320016995 * meter });
                    }
                    {
                        skConstraint(sketch, "DDdMMp3gYRxI", { "constraintType" : ConstraintType.DISTANCE, "index" : "21", "name" : "", "localFirst" : "vNZaYuuZX5WG.left", "localSecond" : "vNZaYuuZX5WG.right", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('mag_width') + (lookup('mag_xy_tolerance') * 2)), 'expression' : "#mag_width+ (#mag_xy_tolerance *2)" }.value, "halfSpace0" : DimensionHalfSpace.RIGHT, "halfSpace1" : DimensionHalfSpace.LEFT, "labelRatio" : 0.5000000000000001, "labelDistance" : -0.026079856265012396 * meter });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.24.1.0.coi0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "77", "name" : "", "localFirst" : "gAMrxUh9DdFH.0.1.0.start", "localSecond" : "gAMrxUh9DdFH.1.1.0.start", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.24.1.0.coi1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "78", "name" : "", "localFirst" : "gAMrxUh9DdFH.0.1.0.end", "localSecond" : "gAMrxUh9DdFH.3.1.0.start", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.24.1.0.coi2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "79", "name" : "", "localFirst" : "gAMrxUh9DdFH.2.1.0.start", "localSecond" : "gAMrxUh9DdFH.1.1.0.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.24.1.0.coi3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "80", "name" : "", "localFirst" : "gAMrxUh9DdFH.2.1.0.end", "localSecond" : "gAMrxUh9DdFH.3.1.0.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.24.1.0.coi4", { "constraintType" : ConstraintType.COINCIDENT, "index" : "81", "name" : "", "localFirst" : "gAMrxUh9DdFH.7.1.0.start", "localSecond" : "gAMrxUh9DdFH.4.1.0.start", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.24.1.0.coi5", { "constraintType" : ConstraintType.COINCIDENT, "index" : "82", "name" : "", "localFirst" : "gAMrxUh9DdFH.7.1.0.end", "localSecond" : "gAMrxUh9DdFH.6.1.0.start", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.24.1.0.coi6", { "constraintType" : ConstraintType.COINCIDENT, "index" : "83", "name" : "", "localFirst" : "gAMrxUh9DdFH.5.1.0.start", "localSecond" : "gAMrxUh9DdFH.4.1.0.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.24.1.0.coi7", { "constraintType" : ConstraintType.COINCIDENT, "index" : "84", "name" : "", "localFirst" : "gAMrxUh9DdFH.5.1.0.end", "localSecond" : "gAMrxUh9DdFH.6.1.0.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.hv1", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "20", "name" : "", "localFirst" : "gAMrxUh9DdFH.direction1", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.len1.c1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "93", "name" : "", "localFirst" : "gAMrxUh9DdFH.direction1.start", "localSecond" : "1h5lzhu1DqW3.left.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.len1.c2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "94", "name" : "", "localFirst" : "gAMrxUh9DdFH.direction1.end", "localSecond" : "gAMrxUh9DdFH.1.1.0.end", "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.len1.length", { "constraintType" : ConstraintType.LENGTH, "index" : "5", "name" : "", "localFirst" : "gAMrxUh9DdFH.direction1", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('switch_width') + (lookup('switch_tolerance') * 2) + lookup('switch_pitch')), 'expression' : "#switch_width + (#switch_tolerance * 2) + #switch_pitch" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.49999999999999983, "labelDistance" : 0.009161152034325399 * meter, "sketchToolType" : SketchToolType.PATTERN });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.pattern", { "constraintType" : ConstraintType.LINEAR_PATTERN, "index" : "2", "name" : "", "patterng" : { 'value' : try(roundWithinTolerance(24)), 'expression' : "" }.value, "patternc1" : { 'value' : try(roundWithinTolerance(lookup('num_switches_left') + 1)), 'expression' : "#num_switches_left  +  1" }.value, "previouspatternc1" : { 'value' : try(roundWithinTolerance(3)), 'expression' : "" }.value, "maximumpatterng" : { 'value' : try(roundWithinTolerance(24)), 'expression' : "" }.value, "patternc2" : { 'value' : try(roundWithinTolerance(1)), 'expression' : "" }.value, "previouspatternc2" : { 'value' : try(roundWithinTolerance(1)), 'expression' : "" }.value, "localInstance0,0,0" : "1h5lzhu1DqW3.bottom", "localInstance1,0,0" : "1h5lzhu1DqW3.bottom.start", "localInstance2,0,0" : "1h5lzhu1DqW3.bottom.end", "localInstance3,0,0" : "1h5lzhu1DqW3.left", "localInstance4,0,0" : "1h5lzhu1DqW3.left.start", "localInstance5,0,0" : "1h5lzhu1DqW3.left.end", "localInstance6,0,0" : "1h5lzhu1DqW3.top", "localInstance7,0,0" : "1h5lzhu1DqW3.top.start", "localInstance8,0,0" : "1h5lzhu1DqW3.top.end", "localInstance9,0,0" : "1h5lzhu1DqW3.right", "localInstance10,0,0" : "1h5lzhu1DqW3.right.start", "localInstance11,0,0" : "1h5lzhu1DqW3.right.end", "localInstance12,0,0" : "vNZaYuuZX5WG.left", "localInstance13,0,0" : "vNZaYuuZX5WG.left.start", "localInstance14,0,0" : "vNZaYuuZX5WG.left.end", "localInstance15,0,0" : "vNZaYuuZX5WG.top", "localInstance16,0,0" : "vNZaYuuZX5WG.top.start", "localInstance17,0,0" : "vNZaYuuZX5WG.top.end", "localInstance18,0,0" : "vNZaYuuZX5WG.right", "localInstance19,0,0" : "vNZaYuuZX5WG.right.start", "localInstance20,0,0" : "vNZaYuuZX5WG.right.end", "localInstance21,0,0" : "vNZaYuuZX5WG.bottom", "localInstance22,0,0" : "vNZaYuuZX5WG.bottom.start", "localInstance23,0,0" : "vNZaYuuZX5WG.bottom.end", "localInstance0,1,0" : "gAMrxUh9DdFH.0.1.0", "localInstance1,1,0" : "gAMrxUh9DdFH.0.1.0.start", "localInstance2,1,0" : "gAMrxUh9DdFH.0.1.0.end", "localInstance3,1,0" : "gAMrxUh9DdFH.1.1.0", "localInstance4,1,0" : "gAMrxUh9DdFH.1.1.0.start", "localInstance5,1,0" : "gAMrxUh9DdFH.1.1.0.end", "localInstance6,1,0" : "gAMrxUh9DdFH.2.1.0", "localInstance7,1,0" : "gAMrxUh9DdFH.2.1.0.start", "localInstance8,1,0" : "gAMrxUh9DdFH.2.1.0.end", "localInstance9,1,0" : "gAMrxUh9DdFH.3.1.0", "localInstance10,1,0" : "gAMrxUh9DdFH.3.1.0.start", "localInstance11,1,0" : "gAMrxUh9DdFH.3.1.0.end", "localInstance12,1,0" : "gAMrxUh9DdFH.4.1.0", "localInstance13,1,0" : "gAMrxUh9DdFH.4.1.0.start", "localInstance14,1,0" : "gAMrxUh9DdFH.4.1.0.end", "localInstance15,1,0" : "gAMrxUh9DdFH.5.1.0", "localInstance16,1,0" : "gAMrxUh9DdFH.5.1.0.start", "localInstance17,1,0" : "gAMrxUh9DdFH.5.1.0.end", "localInstance18,1,0" : "gAMrxUh9DdFH.6.1.0", "localInstance19,1,0" : "gAMrxUh9DdFH.6.1.0.start", "localInstance20,1,0" : "gAMrxUh9DdFH.6.1.0.end", "localInstance21,1,0" : "gAMrxUh9DdFH.7.1.0", "localInstance22,1,0" : "gAMrxUh9DdFH.7.1.0.start", "localInstance23,1,0" : "gAMrxUh9DdFH.7.1.0.end", "localDirection1" : "gAMrxUh9DdFH.direction1", "labelDistance" : 0.01295582545390485 * meter, "labelAngle" : -0.7853981633974487 * radian, "sketchToolType" : SketchToolType.PATTERN, "localInstance0,2,0" : "gAMrxUh9DdFH.0.2.0", "localInstance3,2,0" : "gAMrxUh9DdFH.3.2.0", "localInstance6,2,0" : "gAMrxUh9DdFH.6.2.0", "localInstance9,2,0" : "gAMrxUh9DdFH.9.2.0", "localInstance12,2,0" : "gAMrxUh9DdFH.12.2.0", "localInstance15,2,0" : "gAMrxUh9DdFH.15.2.0", "localInstance18,2,0" : "gAMrxUh9DdFH.18.2.0", "localInstance21,2,0" : "gAMrxUh9DdFH.21.2.0", "localInstance1,2,0" : "gAMrxUh9DdFH.0.2.0.start", "localInstance2,2,0" : "gAMrxUh9DdFH.0.2.0.end", "localInstance4,2,0" : "gAMrxUh9DdFH.3.2.0.start", "localInstance5,2,0" : "gAMrxUh9DdFH.3.2.0.end", "localInstance7,2,0" : "gAMrxUh9DdFH.6.2.0.start", "localInstance8,2,0" : "gAMrxUh9DdFH.6.2.0.end", "localInstance10,2,0" : "gAMrxUh9DdFH.9.2.0.start", "localInstance11,2,0" : "gAMrxUh9DdFH.9.2.0.end", "localInstance13,2,0" : "gAMrxUh9DdFH.12.2.0.start", "localInstance14,2,0" : "gAMrxUh9DdFH.12.2.0.end", "localInstance16,2,0" : "gAMrxUh9DdFH.15.2.0.start", "localInstance17,2,0" : "gAMrxUh9DdFH.15.2.0.end", "localInstance19,2,0" : "gAMrxUh9DdFH.18.2.0.start", "localInstance20,2,0" : "gAMrxUh9DdFH.18.2.0.end", "localInstance22,2,0" : "gAMrxUh9DdFH.21.2.0.start", "localInstance23,2,0" : "gAMrxUh9DdFH.21.2.0.end" });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.2.0.coi7", { "constraintType" : ConstraintType.COINCIDENT, "index" : "95", "name" : "", "localFirst" : "gAMrxUh9DdFH.18.2.0.start", "localSecond" : "gAMrxUh9DdFH.21.2.0.end" });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.2.0.coi6", { "constraintType" : ConstraintType.COINCIDENT, "index" : "96", "name" : "", "localFirst" : "gAMrxUh9DdFH.15.2.0.end", "localSecond" : "gAMrxUh9DdFH.18.2.0.end" });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.2.0.coi5", { "constraintType" : ConstraintType.COINCIDENT, "index" : "97", "name" : "", "localFirst" : "gAMrxUh9DdFH.12.2.0.end", "localSecond" : "gAMrxUh9DdFH.15.2.0.start" });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.2.0.coi4", { "constraintType" : ConstraintType.COINCIDENT, "index" : "98", "name" : "", "localFirst" : "W5o7WVF6PNS6.9.2.0.start", "localSecond" : "W5o7WVF6PNS6.15.2.0.start" });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.2.0.coi3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "99", "name" : "", "localFirst" : "W5o7WVF6PNS6.6.2.0.start", "localSecond" : "W5o7WVF6PNS6.21.2.0.end" });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.2.0.coi0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "100", "name" : "", "localFirst" : "W5o7WVF6PNS6.0.2.0.start", "localSecond" : "W5o7WVF6PNS6.21.2.0.start" });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.2.0.coi2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "101", "name" : "", "localFirst" : "W5o7WVF6PNS6.3.2.0.end", "localSecond" : "W5o7WVF6PNS6.6.2.0.end" });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.2.0.coi5", { "constraintType" : ConstraintType.COINCIDENT, "index" : "102", "name" : "", "localFirst" : "W5o7WVF6PNS6.9.2.0.end", "localSecond" : "W5o7WVF6PNS6.12.2.0.start" });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.2.0.coi0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "103", "name" : "", "localFirst" : "gAMrxUh9DdFH.0.2.0.start", "localSecond" : "gAMrxUh9DdFH.3.2.0.start" });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.2.0.coi6", { "constraintType" : ConstraintType.COINCIDENT, "index" : "104", "name" : "", "localFirst" : "W5o7WVF6PNS6.12.2.0.end", "localSecond" : "W5o7WVF6PNS6.18.2.0.end" });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.2.0.coi1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "105", "name" : "", "localFirst" : "W5o7WVF6PNS6.0.2.0.end", "localSecond" : "W5o7WVF6PNS6.3.2.0.start" });
                    }
                    {
                        skConstraint(sketch, "W5o7WVF6PNS6.2.0.coi7", { "constraintType" : ConstraintType.COINCIDENT, "index" : "106", "name" : "", "localFirst" : "W5o7WVF6PNS6.15.2.0.end", "localSecond" : "W5o7WVF6PNS6.18.2.0.start" });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.2.0.coi4", { "constraintType" : ConstraintType.COINCIDENT, "index" : "107", "name" : "", "localFirst" : "gAMrxUh9DdFH.12.2.0.start", "localSecond" : "gAMrxUh9DdFH.21.2.0.start" });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.2.0.coi1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "108", "name" : "", "localFirst" : "gAMrxUh9DdFH.0.2.0.end", "localSecond" : "gAMrxUh9DdFH.9.2.0.start" });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.2.0.coi2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "109", "name" : "", "localFirst" : "gAMrxUh9DdFH.3.2.0.end", "localSecond" : "gAMrxUh9DdFH.6.2.0.start" });
                    }
                    {
                        skConstraint(sketch, "gAMrxUh9DdFH.2.0.coi3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "110", "name" : "", "localFirst" : "gAMrxUh9DdFH.6.2.0.end", "localSecond" : "gAMrxUh9DdFH.9.2.0.end" });
                    }
                    {
                        skConstraint(sketch, "ilE1DLjavjuI.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "111", "name" : "", "localFirst" : "ilE1DLjavjuI.start", "localSecond" : "EFh1IGCAhwQi.start" });
                    }
                    {
                        skConstraint(sketch, "ilE1DLjavjuI.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "112", "name" : "", "localFirst" : "ilE1DLjavjuI.end", "localSecond" : "R4fthY4SaoUb.start" });
                    }
                    skSetInitialGuess(sketch, initialGuessFVs9cd2oSNlD0KP_0);
                    skSolve(sketch);
                }
            };
        try(features.FVs9cd2oSNlD0KP_0(id));
        features.F2ji2J7PGpLboRN_0 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var fpbUfQmZeZVfaR_query;
                    fpbUfQmZeZVfaR_query=qCompressed(1.0,"&3e7$eJylkVl32jAQhf/M9NEcj7yAH40X8Emx6chJCy85xlJBLWDHKGnor69YkqYL6Wn7JmnufHfu6M3Qg3f3sttPPI4MhNpVm4Va3ldaNdu40lWIE/Zrpdy3kg+gLKbF22I0M0e51UoruQv9kEW2oS1AyE49SJF2zeb4Up1E+0PzsILk+cJdSOJRYlpWaqebbn/GR5SEZVbkptC0sjs6Z2LIIBMhcsSeD+nNLqgFa3i+ju2r6a39RXWyaHkAd4dUR5AAfpWU0fg2ycusnHEJu89S16vTAJngdc8FMffYtUqFWzjztfyoySOf+jSgIKqNmSV6fVCbtlNbTTXvQzaZUpaXMR7j+uQeIn5vogXVJEgatoUuqLrNPUE0m+QO2YedEhIjJ2SnbZEbXgJYGJici0brZkM4uGQD4UilD2K2jXD06aJouVyrWI/nQyWRvKjPfbhJqEw+nJXITLgATLCEeBKVsYXPA7JX3E3IBeDKW39d3WN8994xIma/gBJDwpetx0d0iDlPCzT4n1dDbPAk+MMEFsOeA7pp/9bYe+XnsGcxv4ewDCfd4/UqiEU6RvtfWBbzzjSL9YH9P8L5DeIQ03ypC2kYJT/WvgEcCSME",id);
                    var fcfTrHmeDHovZD_query;
                    fcfTrHmeDHovZD_query=qCompressed(1.0,"&28b$eJyFkV9PwjAUxb/M9VGydn+Ax7EVXHAb3jYqvpiyNtAobG5Vg5/eMtRgNPrStL33/M657dkkhKtn3e7zkBMKynRyuzLrZ2lNvUullTHJ6c+K2Deaj0CUi/KynC3dVu+ssUZ3cRDTxHO0FSjdmhetpm297W/ksWl/EE8ksK8DD4ClM+YkG9PZut1/4BNkscjKwhXqRre9c6YmFDIVE07IIILpdTeuFK158Zh688W992paXTZ8DE+HqXqQAj5nIrm4Z4XIxJJr6B60rTbHAJni1cAHUzVFqBCXeeHbusEQIxziCMdJ5bzO1WAIZtu0Zmex4kPI8gVmhUjPyXFcDPr1S4UrrFCh5hXcsEjmqdesH+3qpIFQ10H89H/A7OotJ203mt91258AJGFPiP4gsLDpFhMl33IToXf4UiRI0Y/pp3nsbkZICYbJkEdwzVCw2xMj96BuXIacJQKJPJWpXobUP81FA5fr17C9QwDTOGHfa+9OS8X2",id);
                    var QdYbGgWsoEERca_query;
                    QdYbGgWsoEERca_query=qCompressed(1.0,"&43f$eJydUtty2jAQ/Zntoz2WfINHYwvqEBuycqHkJWMsJagN2LFFO/TrKy6TNgkwnT5Z0p49l11/Gvhwt5XtLvM5oSBUV66X6mlbalVvklKXEcnox0qxayTvQTGZTm4no4U5yo1WWskuCiIaO1nAlyBkq35IMWzrtXnxeXkE7fbNgxLY64V7wJIRMy0r1em63Z3oY2RRkU5yU6gb2R6UUzGgkIqIcELsAIazrl8JWvP8OXHG0wfnp2rlpOF9eNmnOhAJ4GNWxJ8fWF6kxYJL6L5LXa2OBlLBK9sHVTW5LxAXWe626mml0dlHR4IU3YgeQvnoRd7h62OAIfawj0usUKDkFnGNnWWtdb1G4lxAVTDaZqRON483tzi/CIrHX27u0dnqjWzQj0MewIxhwb4ekXFl4lvCDkGtm1ZtNFYmcJoXDDmLi8Qir3bpFS+2RQJI+i1ls7BJX56/GRDpnVCkNDCyTMhfnfs3HkKaTdGIIXWPq0bPiLwfF1JzEf/iwyLSdkHXzRl5o/FOH2nvuvBJ0L2yJ4sSuLqju18Zabve+L5bnze1z/do8l02ZzxcnDsxDgKbwNyvw/lsGExzHpggH7n+DPBSGuqf2CwaAv0fO28pwrMUh3/Qg2EUs7e133/gPPw=",id);
                    annotation { "Feature Name" : "Extrude - Total" }
                    extrude(context, id + "F2ji2J7PGpLboRN_0", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.NEW, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([fpbUfQmZeZVfaR_query, fcfTrHmeDHovZD_query, QdYbGgWsoEERca_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.BLIND, "oppositeDirection" : false, "depth" : { 'value' : try(lookup('asm_total_depth')), 'expression' : "#asm_total_depth" }.value, "endBoundEntityFace" : qUnion([]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : false, "startOffsetBound" : StartOffsetType.BLIND, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([]), "symmetric" : false, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.F2ji2J7PGpLboRN_0(id));
        features.FvpopEYhEh3YsGd_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var tBsFNrjviaqMYQ_query;
                    tBsFNrjviaqMYQ_query=qCompressed(1.0,"&292$eJyNUNtymzAQ/Rn10R5W5hIeMQhKE8BdqWmclwyWlEZtDRTkdPz3lXHSS5rO9G2157ZHb9YBeX/Q47EKOFCizNTud+bTobWm77LWtglU9G9EHAfNL4hoNs1VU2zdqDtrrNFT4ic09aqQ74jSo3nUKh/7vdsEvD2TjifxuiXs54P7hGUFc5IHM9l+PD7Zp8gSUTa1A/pBj3NyqdaUlCoBDrAMSX49xVLRntdfM+9yc+d9N6NuBh6Tb6dWs5Ei/JKJ9O0dq0UptlyT6Yu28uF8QKm4dEbqNqAfTK78ZnW7663t9+iduiMgxVVC51YBunYI3jwGGGKEFxjjDiUq1FySpDD5o9p2KRSfMUgjHpJrhoLdnJmpdHcv1DIiZj+MprMo3aVlLRhylopsAf8XUxwq6Mvu/t0VfnQE8J8YEDoKRBn8pjrteETKaoMuCCF+jviX+3IBPjFyqAOFuK3q1QszpD7C/bMLAn0Nj3/h8lX9/Ds+yZOU/Yn9ALEQx5Y=",id);
                    var FOeznLUVRExhqm_query;
                    FOeznLUVRExhqm_query=qCompressed(1.0,"&28e$eJyNUNlSwjAU/Zn4qNPELvBY2hQrluJNAOGFaZsoGaWtbdDBrzcUF9xmfLu559yz5GTgoOutbHaJwzBBQrXZJld320yrqgwznfk4IT8Rvqsl6yGeTtKrdLgwoyy10kq2vu2TwEpcliMhG/UkRdRUG7NxWHYg7fbHgwzRjwezEQ2H1JysVaurZvcmHwD1eZyODVDVsumcYzEgKBY+ZhifuSiatf1CkIqNH0JrNFlZz6qRac366HHfqhMSiI0oDy5WdMxjvmAStfdSF+tDgFiwwgiJpUOmKhJ2er7MK62rDVj77oCBwLlPulYOmHaArW50wAUPetCHHAoQIFmBhtsEV3F5e3kFc3ACj7loRoHTmwMzKEzuU3HmIbWpG1VqKEzSeMwpMBrw8BT/z6an/EY8OPMXvlaGgO03BnYNBXshPrra75iH4mQCxghw/90CMPlGA2If4X+6B6Pp5RKsrS5l/ZvC7adD8Sve/YyNIj+gX7FX1DbHbw==",id);
                    var BlANHoFcqIpRVb_query;
                    BlANHoFcqIpRVb_query=qCompressed(1.0,"&28e$eJyNUMtSwjAU/Zm41GlCH7AsbVo7CMWbCJSN0zYBokCxjTL8vaGIg68Zdzf3nHseueo76P5V1oehwzBBQjX5plDL11yrahvmOvfxkPxE+GEnWRfxdJzepXFmRrnVSivZ+LZPAmvosgIJWas3KaK62piNw/IT6XA87ueIfj6YjWgYU3OyUo2u6sOHfADU50k6MkC1k3XrnIg+QYnwMcP4xkXRpOmVglRstA6twfjR2qtapjvWQy/HVq2QQGxAeXD7SEc84RmTqHmWulydAiSClUZIzB3yoCJhp515UWldbcA6dgcMBDo+aVs5YNoBttrRARc86EIPCihBgGQl8mMVvYlsG+D4CZzAYy6aUOB0dmIGpcl9LW48pDa7Wm01lCZpMuIUGA14iP/nMp92Z/HC3mfe1DYEbH8wsGso2Auv8cXZcck8lAzHYIwAL84egMk3GhCz7J3xP+2Xy7UK9e28r+R3o1bhwqH8FW9/xkaRH9Cv2Dt2FMcC",id);
                    var TBGWzCEHbdDlWi_query;
                    TBGWzCEHbdDlWi_query=qCompressed(1.0,"&21a$eJyNUUtzgjAQ/jPbow7hoXLkEZUqgSbYjl6YQFJNa4VCnOq/L0inl44zPe5+j9399sF34Oksm2vsMGSCUC3/KNT+zLWqTiHX3EOx+RfJrrVkyIKERouIeOs8xCkmISbBlrlQNWqvTvzYepPA6Iw5yJNW+tqrfA74t2A24HCBWQEH1eqqGZozCCj2sighHVDVsrmNjIRvQiQ8xBAaT2D+3LqlMCtGjqGxSnPjSzUyqbvpn/05NyMBbIWzYJljkkXZlklo36UuD8MCkWDl2Aaxc8yNmgs7sXZH+ar7jalNHTqhUzqjnBa0pD3VAlXWxBGUbmNi6aq+wxwhZzzqsrlrhDqYjxHsvbi5bA5uKOZLZNx1c38EI1SA+S+WRe1g0mXrJ+F2IAbFkJsLc/NNmY/TdFGvi4qS3KhqfNHNWUhadI9iLzjN8l74Db1FpsA=",id);
                    annotation { "Feature Name" : "Extrude 1" }
                    extrude(context, id + "FvpopEYhEh3YsGd_1", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.ADD, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([tBsFNrjviaqMYQ_query, FOeznLUVRExhqm_query, BlANHoFcqIpRVb_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.BLIND, "oppositeDirection" : false, "depth" : { 'value' : try(lookup('sp_depth') - lookup('sp_tolerance')), 'expression' : "#sp_depth - #sp_tolerance" }.value, "endBoundEntityFace" : qUnion([]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : false, "startOffsetBound" : StartOffsetType.BLIND, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([]), "symmetric" : false, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([TBGWzCEHbdDlWi_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.FvpopEYhEh3YsGd_1(id));
        features.FQAwrmJegbWP2SZ_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var EowRkFkJReOavK_query;
                    EowRkFkJReOavK_query=qCompressed(1.0,"&227$eJyVUVtvgjAU/jNnjxiKXOQRuShxgDvtlvhkkDbabAoruI1/vwqL2eJeTNPmtN/lfCd9mDvwdBaqzxxKLOCyLY87uT+XnaxPUdmVAcmsW4T1jaAzYMW6eCwWG12KUyc7KdrADqzQ1G474ELJD8ETVR+Hl3Ik9RfxvIT4eqE2xNEi1pKDbLta9T/2IcYBS4tcA3Uj1NA55XMLUh4QSsjEheSl9Stu1TR/i8zVemt+SiWKhvrwfplqMOJAVzELl9s4ZynbUAHtq+iqwxgg5bSakMuCfZCpr+eDH/FkaRMTHXTRwxn6YaX7GXzigTw2Sp46rKgHabbGNGeRQcaR0R7Oqwp3WCFHQQ0y1f4GcfR24TeDWJpCpkicuyzcW4vovhDefyEEOqGnPyMJwvgv9g3+dqTq",id);
                    var MNbNSJAaparBSS_query;
                    MNbNSJAaparBSS_query=qCompressed(1.0,"&227$eJyVUV1vgjAU/TN3jxiKFPQRoTjiBHbbaHwySJvZbAoD3MK/X0VjtrgX07S57fm456ZPMwqvJ9X0S8qJA1K3xWGn305Fp6tjVHRFQJbOPSL6WvEJiCzPXrL5xpTq2OlOqzZwAye0jdsOpGr0l5JxUx2Gl+JC6s/iWQHsduEusGjOjGSv265q+qt9iCwQSZYaoKpVM3RO5MyBRAaEEzLyIF6101I6FU8/InuRb+1v3ais5lP4PE81GEngCybC5y1LRSI2XEH7rrpyfwmQSF6OyHnBmlb+ehV7ecq9MbGRooc+TnAalqafJUc+6EPd6GOHJfchWeaYpCKyyGVkdIfzpsIdlihRcYuMjb9FqNkeuL8YxDEUMkZCH7Kg9xbRYyG8/0IopKFvPiMOQvYX+wFnraRd",id);
                    var ZRFWtNGPufSlcN_query;
                    ZRFWtNGPufSlcN_query=qCompressed(1.0,"&2af$eJylkl1vgjAUhv/M2SWGIh9yiXwocYA7ZUu8MkgbbTaFFdzGv18FY7boxczStGn7vu9zepI+TC14OnLZJRYlBjDRFPuN2B6LVlSHoGgLjyTGtZJ3NacTyLNl9pjNVmrLD61oBW882zN8XdE2wLgUH5xFstr3N8Vg6k7haQHh5UBNCINZqCI70bSV7M54H0Mvj7NUCVXNZV85ZlMDYuYRSsjIhuilcUtmVDR9C/TFcq1/CsmzmrrwfuqqBzGgizD35+swzeN8RTk0r7wtd8MDYkbLEVFDIyZsvUR+Pe/cgEVznaCFNjo4QdcvVUGNjRwQ+1qKQ4sldSBOlhineaCRoWc0+/WSwg2WyJBTjYwV3jpPE35YiKE8ZBz8BUEuAOOagKS4C2H9H2HfQlh3IZybCLR8R32KyPPD39o3vWzHdQ==",id);
                    var XgcxEGspaAFLIi_query;
                    XgcxEGspaAFLIi_query=qCompressed(1.0,"&2af$eJylklFvgjAQx7/M7RFDkYI+IhRHnMCuROOTQdrMZlMY4Ba+/Soat0UfXJamzbX///3uLunDhMLzQdbdnHJigVBNvtuol0PeqnIf5G3ukbl1rWRdJfkIsiRNnpLpSody36pWycZzPMs3NW0DQtbqQ4qwLnf9S34ydcfkSQ7scuE2sGDKdMpWNW1Zd2e8j8zLoiTWQlnJuq8ciYkFkfAIJ2TgQLhoxoWwSh6/BeYsXZufqpZJxcfwfpyqBwngM5b5j2sWZ1G24hKaV9kW21MDkeDFgOhlEBuWtHSXi9BJY+6YBCk66OIIx36hCxpi4ILaVbXat1hwF6J5ilGcBeQ0Mtr9eUnCDRYoUHKDDDWdnrcNPyzE0h4yRELvYJALwbpGBMY9bXwj6K0u5J8Qzv8R7k0EUt/VnyL0fPZb+wLd+cck",id);
                    var uRrGzkTTHumsgC_query;
                    uRrGzkTTHumsgC_query=qCompressed(1.0,"&2b1$eJylkm9vgjAQxr/M7SWEVgv6kr+OOIFdicZXBmkzm01hgFv49qu4mC36YmZp2vR69/zuuaQPHoPno2z6BeOEglBtsd+ql2PRqeoQFF3hkgW9zuR9LfkE8jRLn9LZWl/loVOdkq1ru9S3NG0LQjbqQ4qoqfbDS3Eu6k9ir4DwEvAxhMEs1JKdaruq6b/xPoZuHqeJTlS1bIbOsfAoxMIlnBDThmjZTktBK568BdY821ifqpFpzafwfppqAAng8zD3Hzdhksf5mktoX2VX7s4GYsFLk5wWrFjlrJaRnSXcHlELGdro4ASnfqn7GcJ0QO3rRh06LLkD8SLDOMkDcp4Yx8N5EeEWSxQouUFGJjUNwvS2gdAfJTookYyQsPsYk2tGYPzFB7kw7Fs25H022P8ZlNxkIPMd/S0i1w9/574AhlbHmg==",id);
                    var tILDBLSikRZzdH_query;
                    tILDBLSikRZzdH_query=qCompressed(1.0,"&22a$eJyVkV9vgjAUxb/M3SOEWy3II/LHESewttH4ZJA2s9kUBriFb78OF7PFPWwPbdqec3/n3vRuTuHxrNphRTkSkLorj3v9dC57XZ+isi8DXJFbRQyN4jMQeZE/5IutOapTr3utumAakNAxtD1I1eo3JZO2Po4v5cU0fBbPS4ivFz6FOFrEpuSgu75uhy98yOJApHlmhLpR7ZicyjmBVAbIEW0XknXnV5LUPHuJnGWxc951q/KG+/D6OdUIksCXsQjvd3EmUrHlCrpn1VeHSwOp5JVNbLQRNrT2NuvELTLuIiEOo8xlHpsxP6xMoCVtD/SxafWpZxX3IF0VLM1EZOFlZjYd92sV27OKSaa4hRMTYSE1ywWcfbMgMR6cRH9B4BXh3xIY0v91QX9jlIyGnvmPJAjjn9oHOcKk9g==",id);
                    var yPCxTYhLvHGzJD_query;
                    yPCxTYhLvHGzJD_query=qCompressed(1.0,"&2b1$eJydkl9vgjAUxb9M9yjhVgF5RP4ocYC7ZUt8MkgbbTaFFdzGt1/FxW3Rh7mHNm3PPb97btK7iUUeDkJ1icWAEi6bYreWm0PRymofFG3hQUIvlbyrBRuTPFtk99l0qY9i38pWisazPeqbmrYmXCj5Jnikql3/UpyKuqN5UpDwfGEjEgbTUFu2smkr1X3hfQy9PM5SLVS1UH3nmE8oibkHDMCwSfTUuCWnFUtfAnO+WJnvUomsZi55PU7Vgzhh8zD3Z6swzeN8yQRpnkVbbk8BYs5KgxpgANl4ifp43LoBj2ZAqYkW2ujgGF2/1A0H3HCI3NVK7lssmUPiZIFxmgdwGhlH/X424RpL5CjYAIaaPwBLL5sMf1QA1SUwRLBuQriXiGDwlxT0jADrvzG+GRSuMcRtOcZXGWj5jv4WkeeHv7VPQyjH9g==",id);
                    var xQpWFhEeCIRVQI_query;
                    xQpWFhEeCIRVQI_query=qCompressed(1.0,"&22b$eJyVUVtvgjAU/jPdI4RTReQRuShxgDvtlvhkkDbabAoruI1/v4qL2eJefGjT9rue9GHmkqeT1H3mMqBEqLY8bNXuVHaqPkZlVwaQ0VuE941kU8KLVfFYzNfmKI+d6pRsg3FAQ8e4bYmQWn1Ikej6MLyUF1J/Fs9KEl8vbEziaB4byV61Xa37H/sQ44CnRW6AupF6SE7FjJJUBMAA7AlJXlq/ErRm+VvkLFcb51NpWTTMJ+/nqQYjQdgy5uFiE+c85WsmSfsqu2p/KZAKVtnUBhvILsj01/Pej0SyAEoddHGCHk7RDysTaAnbI+rQaHXssGIeSbMVpjmPLLjMjONhv6pwixUKlMyCkYmwwDVrQuA3BajhwAjBvcuDwq1HdGeN6X81JLqhZz4kCcL4L/YNaqelrg==",id);
                    var LCvJZDPRsacJwm_query;
                    LCvJZDPRsacJwm_query=qCompressed(1.0,"&21a$eJyNUUtzgjAQ/jPbow7hoXLkEZUqgSbYjl6YQFJNa4VCnOq/L0inl44zPe5+j9399sF34Oksm2vsMGSCUC3/KNT+zLWqTiHX3EOx+RfJrrVkyIKERouIeOs8xCkmISbBlrlQNWqvTvzYepPA6Iw5yJNW+tqrfA74t2A24HCBWQEH1eqqGZozCCj2sighHVDVsrmNjIRvQiQ8xBAaT2D+3LqlMCtGjqGxSnPjSzUyqbvpn/05NyMBbIWzYJljkkXZlklo36UuD8MCkWDl2Aaxc8yNmgs7sXZH+ar7jalNHTqhUzqjnBa0pD3VAlXWxBGUbmNi6aq+wxwhZzzqsrlrhDqYjxHsvbi5bA5uKOZLZNx1c38EI1SA+S+WRe1g0mXrJ+F2IAbFkJsLc/NNmY/TdFGvi4qS3KhqfNHNWUhadI9iLzjN8l74Db1FpsA=",id);
                    annotation { "Feature Name" : "Extrude - Backplate" }
                    extrude(context, id + "FQAwrmJegbWP2SZ_1", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.ADD, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([EowRkFkJReOavK_query, MNbNSJAaparBSS_query, ZRFWtNGPufSlcN_query, XgcxEGspaAFLIi_query, uRrGzkTTHumsgC_query, tILDBLSikRZzdH_query, yPCxTYhLvHGzJD_query, xQpWFhEeCIRVQI_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.BLIND, "oppositeDirection" : false, "depth" : { 'value' : try(lookup('asm_wall_thickness')), 'expression' : "#asm_wall_thickness" }.value, "endBoundEntityFace" : qUnion([]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : false, "startOffsetBound" : StartOffsetType.BLIND, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([]), "symmetric" : false, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([LCvJZDPRsacJwm_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.FQAwrmJegbWP2SZ_1(id));
        features.F0QQXgQkgHatdhM_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var PRzzwAOmKuGdYn_query;
                    PRzzwAOmKuGdYn_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.-13$gAMrxUh9DdFH01C0M5R4R5R6R7R8RaRbRcRdS-12.1.-14.-13$3R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var PEdHPcCytXOQBe_query;
                    PEdHPcCytXOQBe_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.-13$gAMrxUh9DdFH01C0M5R4R5R6R7R8RaRbRcRdS-12.-14.-14.-13$R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var eMWIMuegQjrYiu_query;
                    eMWIMuegQjrYiu_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.-13.1$gAMrxUh9DdFH10C0M5R4R5R6R7R8RaRbRcRdS-12.1.-13.-14$2R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var lubEzOPCeYItxG_query;
                    lubEzOPCeYItxG_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$gAMrxUh9DdFH210C0M5R4R5R6R7R8RaRbRcRdS-12.1.-14.-15$3R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var QnGKclScvLBGPH_query;
                    QnGKclScvLBGPH_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.-13$W5o7WVF6PNS601C0M5R4R5R6R7R8RaRbRcRdS-12.-14.-14.-13$R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var mwbhTjDeGpYmTn_query;
                    mwbhTjDeGpYmTn_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.-13$W5o7WVF6PNS601C0M5R4R5R6R7R8RaRbRcRdS-12.1.-14.-13$7R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var xPuGyDMfeZZcDW_query;
                    xPuGyDMfeZZcDW_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$W5o7WVF6PNS6210C0M5R4R5R6R7R8RaRbRcRdS-12.1.-14.-15$7R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var PcwQrpJwQxjsEi_query;
                    PcwQrpJwQxjsEi_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.-13.1$W5o7WVF6PNS610C0M5R4R5R6R7R8RaRbRcRdS-12.1.-13.-14$2R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var ZbeINDSwljNQki_query;
                    ZbeINDSwljNQki_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$W5o7WVF6PNS6320C0M5R4R5R6R7R8RaRbRcRdS-12.-15.-14.-15$R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var wBkcGLWidEnXif_query;
                    wBkcGLWidEnXif_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.-13$W5o7WVF6PNS602C0M5R4R5R6R7R8RaRbRcRdS-12.2.-14.-13$21R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var MYvLAvopAtMkIk_query;
                    MYvLAvopAtMkIk_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$W5o7WVF6PNS6620C0M5R4R5R6R7R8RaRbRcRdS-12.2.-14.-15$21R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var xfgYEvXoYZvJvW_query;
                    xfgYEvXoYZvJvW_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$W5o7WVF6PNS6320C0M5R4R5R6R7R8RaRbRcRdS-12.1.-14.-15$6R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var vnUDCbhacBNGJK_query;
                    vnUDCbhacBNGJK_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$gAMrxUh9DdFH920C0M5R4R5R6R7R8RaRbRcRdS-12.-15.-14.-15$R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var tzWavrcvTFLSTR_query;
                    tzWavrcvTFLSTR_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$gAMrxUh9DdFH920C0M5R4R5R6R7R8RaRbRcRdS-12.1.-14.-15$6R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var vmrDBpKVrecXSf_query;
                    vmrDBpKVrecXSf_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$gAMrxUh9DdFH320C0M5R4R5R6R7R8RaRbRcRdS-12.1.-14.-15$6R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var pvbIrAUAxfxZMf_query;
                    pvbIrAUAxfxZMf_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$gAMrxUh9DdFH320C0M5R4R5R6R7R8RaRbRcRdS-12.-15.-14.-15$R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    annotation { "Feature Name" : "Fillet - Switch Recess" }
                    fillet(context, id + "F0QQXgQkgHatdhM_1", { "filletType" : FilletType.EDGE, "entities" : qUnion([PRzzwAOmKuGdYn_query, PEdHPcCytXOQBe_query, eMWIMuegQjrYiu_query, lubEzOPCeYItxG_query, QnGKclScvLBGPH_query, mwbhTjDeGpYmTn_query, xPuGyDMfeZZcDW_query, PcwQrpJwQxjsEi_query, ZbeINDSwljNQki_query, wBkcGLWidEnXif_query, MYvLAvopAtMkIk_query, xfgYEvXoYZvJvW_query, vnUDCbhacBNGJK_query, tzWavrcvTFLSTR_query, vmrDBpKVrecXSf_query, pvbIrAUAxfxZMf_query]), "side1Face" : qUnion([]), "side2Face" : qUnion([]), "centerFaces" : qUnion([]), "tangentPropagation" : true, "blendControlType" : BlendControlType.RADIUS, "crossSection" : FilletCrossSection.CIRCULAR, "radius" : { 'value' : try(lookup('switch_width') / 2), 'expression' : "#switch_width/2" }.value, "nonCircularRadius" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "width" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "rho" : { 'value' : try(0.5), 'expression' : "0.5" }.value, "magnitude" : { 'value' : try(0.5), 'expression' : "0.5" }.value, "defaultsChanged" : false, "isAsymmetric" : false, "otherRadius" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipAsymmetric" : false, "isPartial" : false, "startPartialType" : EndTypePartialFillet.PERCENTAGE, "startPartialOffset" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "startPartialEntity" : qUnion([]), "partialFirstEdgeTotalParameter" : { 'value' : try(0.01), 'expression' : "0.01" }.value, "partialOppositeParameter" : true, "useTrimmedFirstBound" : false, "secondBound" : false, "endPartialType" : EndTypePartialFillet.PERCENTAGE, "endPartialOffset" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endPartialEntity" : qUnion([]), "useTrimmedSecondBound" : false, "partialSecondEdgeTotalParameter" : { 'value' : try(0.99), 'expression' : "0.99" }.value, "isVariable" : false, "vertexSettings" : [], "pointOnEdgeSettings" : [], "smoothTransition" : false, "allowEdgeOverflow" : true, "keepEdges" : qUnion([]), "smoothCorners" : false, "smoothCornerExceptions" : qUnion([]) });
                }
            };
        try(features.F0QQXgQkgHatdhM_1(id));
        features.FNWvP1gVAL8wWA5 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FNWvP1gVAL8wWA5", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_corner_fillets", "lengthValue" : { 'value' : try(20 * millimeter), 'expression' : "20 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(20 * millimeter), 'expression' : "20 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FNWvP1gVAL8wWA5(id));
        features.FphbnDYycJGzlPY_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var ABGRmaAlVZdGiG_query;
                    ABGRmaAlVZdGiG_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$R4fthY4SaoUbC0M5R4R5R6R7R8RaRbRcRdSc$E5psPBdazMi6R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var ZqAwPvkXtTspGT_query;
                    ZqAwPvkXtTspGT_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$E5psPBdazMi6C0M5R4R5R6R7R8RaRbRcRdSc$qi2Ta53CvWH1R4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var DpCnFKaOVKBruU_query;
                    DpCnFKaOVKBruU_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$EFh1IGCAhwQiC0M5R4R5R6R7R8RaRbRcRdSc$tTOCUaFlARpNR4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    var yJEFsePTGERlll_query;
                    yJEFsePTGERlll_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$R4fthY4SaoUbC0M5R4R5R6R7R8RaRbRcRdSc$ilE1DLjavjuIR4R5R6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_EDGE",id);
                    annotation { "Feature Name" : "Fillet - Asm Corners" }
                    fillet(context, id + "FphbnDYycJGzlPY_1", { "filletType" : FilletType.EDGE, "entities" : qUnion([ABGRmaAlVZdGiG_query, ZqAwPvkXtTspGT_query, DpCnFKaOVKBruU_query, yJEFsePTGERlll_query]), "side1Face" : qUnion([]), "side2Face" : qUnion([]), "centerFaces" : qUnion([]), "tangentPropagation" : true, "blendControlType" : BlendControlType.RADIUS, "crossSection" : FilletCrossSection.CIRCULAR, "radius" : { 'value' : try(lookup('asm_corner_fillets')), 'expression' : "#asm_corner_fillets" }.value, "nonCircularRadius" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "width" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "rho" : { 'value' : try(0.5), 'expression' : "0.5" }.value, "magnitude" : { 'value' : try(0.5), 'expression' : "0.5" }.value, "defaultsChanged" : false, "isAsymmetric" : false, "otherRadius" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipAsymmetric" : false, "isPartial" : false, "startPartialType" : EndTypePartialFillet.PERCENTAGE, "startPartialOffset" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "startPartialEntity" : qUnion([]), "partialFirstEdgeTotalParameter" : { 'value' : try(0.01), 'expression' : "0.01" }.value, "partialOppositeParameter" : true, "useTrimmedFirstBound" : false, "secondBound" : false, "endPartialType" : EndTypePartialFillet.PERCENTAGE, "endPartialOffset" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endPartialEntity" : qUnion([]), "useTrimmedSecondBound" : false, "partialSecondEdgeTotalParameter" : { 'value' : try(0.99), 'expression' : "0.99" }.value, "isVariable" : false, "vertexSettings" : [], "pointOnEdgeSettings" : [], "smoothTransition" : false, "allowEdgeOverflow" : true, "keepEdges" : qUnion([]), "smoothCorners" : false, "smoothCornerExceptions" : qUnion([]) });
                }
            };
        try(features.FphbnDYycJGzlPY_1(id));
        features.FBkbW675oxrRr9L = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    annotation { "Feature Name" : "###name = #value" }
                    assignVariable(context, id + "FBkbW675oxrRr9L", { "initEntities" : qUnion([]), "mode" : VariableMode.ASSIGNED, "variableType" : VariableType.LENGTH, "measurementMode" : VariableMeasurementMode.DISTANCE, "name" : "asm_face_fillets", "lengthValue" : { 'value' : try(2.5 * millimeter), 'expression' : "2.5 mm" }.value, "angleValue" : 0.0 * degree, "numberValue" : 0.0, "anyValue" : { 'value' : try(0), 'expression' : "0" }.value, "value" : { 'value' : try(2.5 * millimeter), 'expression' : "2.5 mm" }.value, "csv" : {} as TableData, "rowAccessType" : AccessType.INDEX, "rowIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowValue" : "", "rowRegexp" : false, "rowMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "rowResult" : ResultType.SINGLE, "rowLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnAccessType" : AccessType.INDEX, "columnIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnValue" : "", "columnRegexp" : false, "columnMinIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnMaxIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "columnResult" : ResultType.SINGLE, "columnLabelIndex" : { 'value' : try(roundWithinTolerance(0)), 'expression' : "" }.value, "useConversionFactor" : false, "conversionFactor" : { 'value' : try(0), 'expression' : "0" }.value, "entityCouple" : qUnion([]), "minmax" : VariableMinMaxSelection.MINIMUM, "extendEntities" : false, "measureFromAxis" : false, "distance" : 0.0 * meter, "xOffset" : 0.0 * meter, "yOffset" : 0.0 * meter, "zOffset" : 0.0 * meter, "componentSelector" : AxisWithCustom.DISTANCE, "customDirection" : qUnion([]), "customOffset" : 0.0 * meter, "lengthEntities" : qUnion([]), "radius" : false, "diameterEntity" : qUnion([]), "description" : "" });
                }
            };
        try(features.FBkbW675oxrRr9L(id));
        features.FGYJ1IbU5lcg1kw_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var IvtGUfdLXBsQnQ_query;
                    IvtGUfdLXBsQnQ_query=qCompressed(1.0,"%B5$QueryM6S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA1C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$E5psPBdazMi6R4R5R6R7S7$isStartFR8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbS8$CAP_EDGE",id);
                    var AbfrPtbXKKoGuv_query;
                    AbfrPtbXKKoGuv_query=qCompressed(1.0,"%B5$QueryM6S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA3C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.4$dZ52UiFd4O3ZleftC0M5R4R5R6R7R8RaRbRcRdSc$qi2Ta53CvWH1C0M5R4R5R6R7R8RaRbRcRdSc$tTOCUaFlARpNR4R5R6R7S7$isStartFR8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbS8$CAP_EDGE",id);
                    var oOEEUxNtOOUDee_query;
                    oOEEUxNtOOUDee_query=qCompressed(1.0,"&2e3$eJyNUtty2jAQ/Rn1MRlLGBsehS27bvAlK+VCXxjbEkEpYNcWofx9ZaAdJkln+qS9nN1zjma/zMbofq+6Y+pxTJDUfbmt9Mu+NLrZhaUpKUnJx444torjEcohiZOMzpchK1gWsixY8ClqOv2id+Wmpzhw0jEvkdoZbY7D1KxE7G/CXcTCmPEKrXVvmu5cnKAAGBVJntlG06ruRJnIGUGJpJhjfOuh6LGf1pI0PNuEzl2xdA66U3lr2X8Odk6LJOJ3TARflywTiVhwhfofytTrs4BE8hqxaI2TOKDrw722RiskVafflIy6ZkuJVe8BJrACh+KUALbaRF7k8zxe2PDkSquekhN0bKGXwIUxeODDBEqooIaBisY6epOLXYDjV7vRDTzuoUcGgj2fsUFlzd3Utz7S27bTOwOVtZNkggFngQhv8P/QfH+aPMcr97Dwn9yBBvDkgsEDCFchvpobatxHSVqAJfrgF/AIsDv4A2JTeSUAVv+UYEQePJTRhkKbfSIAyOidAiD25Ssk4IFdndIflBWoe27KzkSXX7InMEURedXkm1/E7bxqIFs6Tct+mW4vlfVkj4gWy+G6fgMAduLZ",id);
                    var tVQiDeyqmVvlHq_query;
                    tVQiDeyqmVvlHq_query=qCompressed(1.0,"%B5$QueryM6S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA1C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$R4fthY4SaoUbR4R5R6R7S7$isStartFR8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbS8$CAP_EDGE",id);
                    var auYXVMwfQhKjLB_query;
                    auYXVMwfQhKjLB_query=qCompressed(1.0,"&2e3$eJyNUk1z2jAU/DPqMRkkjIGjsIWrBH/kSUnKibEtEasF7MqiKf31lYF2Mk0POenpvdXbXc1+WkzQw1HbUxoKTJAyfbmvzMuxdKY9xKUrKUnJ+4k8dVrgMcqBJzyjq03MCpbFLIvWYo5aa17Modz1FEejdCJKpA/OuNPwalEi9vciAsTihIkKNaZ3rb00ZygCRiXPMz9oO23PlFwtCOKKYoHxbYiWT/28VqQV2S4e3Reb0auxOu88+/fBznmRQuKeyejzhmWSy7XQqP+mXd1cBHAlasSWDeZJRJvXB+ONVkhpa35otbTtnhKvPgRMYAsjilMC2GuTeZGv8mTty7Mro3tKztCJh16LACYQwhRmUEIFNQxUM0Ot2k2ef8nG+I1BFIoQPTGQ7MsFG1Xe3E19O0Vm31lzcFB5OzyTDASLZIw/wgLB1jXrQJTtYzWwAJ5dMXgA4Sq+wW8eDk0xRTwtwBO98wt4DDj4mL/kmOKWH7Z3K3j+LzNgRf3OGsj4HwVA/Cm2SMIjexOlPygv0PTCldYtr7/kIzBHS/LVkLtpkXSrqoVsM2o79tPZo9Lekw8RLTZDun4DGNvjyA==",id);
                    annotation { "Feature Name" : "Fillet - Asm Face" }
                    fillet(context, id + "FGYJ1IbU5lcg1kw_1", { "filletType" : FilletType.EDGE, "entities" : qUnion([IvtGUfdLXBsQnQ_query, AbfrPtbXKKoGuv_query, oOEEUxNtOOUDee_query, tVQiDeyqmVvlHq_query, auYXVMwfQhKjLB_query]), "side1Face" : qUnion([]), "side2Face" : qUnion([]), "centerFaces" : qUnion([]), "tangentPropagation" : true, "blendControlType" : BlendControlType.RADIUS, "crossSection" : FilletCrossSection.CIRCULAR, "radius" : { 'value' : try(lookup('asm_face_fillets')), 'expression' : "#asm_face_fillets" }.value, "nonCircularRadius" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "width" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "rho" : { 'value' : try(0.5), 'expression' : "0.5" }.value, "magnitude" : { 'value' : try(0.5), 'expression' : "0.5" }.value, "defaultsChanged" : false, "isAsymmetric" : false, "otherRadius" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipAsymmetric" : false, "isPartial" : false, "startPartialType" : EndTypePartialFillet.PERCENTAGE, "startPartialOffset" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "startPartialEntity" : qUnion([]), "partialFirstEdgeTotalParameter" : { 'value' : try(0.01), 'expression' : "0.01" }.value, "partialOppositeParameter" : true, "useTrimmedFirstBound" : false, "secondBound" : false, "endPartialType" : EndTypePartialFillet.PERCENTAGE, "endPartialOffset" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endPartialEntity" : qUnion([]), "useTrimmedSecondBound" : false, "partialSecondEdgeTotalParameter" : { 'value' : try(0.99), 'expression' : "0.99" }.value, "isVariable" : false, "vertexSettings" : [], "pointOnEdgeSettings" : [], "smoothTransition" : false, "allowEdgeOverflow" : true, "keepEdges" : qUnion([]), "smoothCorners" : false, "smoothCornerExceptions" : qUnion([]) });
                }
            };
        try(features.FGYJ1IbU5lcg1kw_1(id));
        features.FvkOQ5h690xvzJ1_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var nGdzdaMeMonmJm_query;
                    nGdzdaMeMonmJm_query=qCompressed(1.0,"&227$eJyVUVtvgjAU/jNnjxiKXOQRuShxgDvtlvhkkDbabAoruI1/vwqL2eJeTNPmtN/lfCd9mDvwdBaqzxxKLOCyLY87uT+XnaxPUdmVAcmsW4T1jaAzYMW6eCwWG12KUyc7KdrADqzQ1G474ELJD8ETVR+Hl3Ik9RfxvIT4eqE2xNEi1pKDbLta9T/2IcYBS4tcA3Uj1NA55XMLUh4QSsjEheSl9Stu1TR/i8zVemt+SiWKhvrwfplqMOJAVzELl9s4ZynbUAHtq+iqwxgg5bSakMuCfZCpr+eDH/FkaRMTHXTRwxn6YaX7GXzigTw2Sp46rKgHabbGNGeRQcaR0R7Oqwp3WCFHQQ0y1f4GcfR24TeDWJpCpkicuyzcW4vovhDefyEEOqGnPyMJwvgv9g3+dqTq",id);
                    var SwpzXIHgXNNVjB_query;
                    SwpzXIHgXNNVjB_query=qCompressed(1.0,"&227$eJyVUV1vgjAU/TN3jxiKFPQRoTjiBHbbaHwySJvZbAoD3MK/X0VjtrgX07S57fm456ZPMwqvJ9X0S8qJA1K3xWGn305Fp6tjVHRFQJbOPSL6WvEJiCzPXrL5xpTq2OlOqzZwAye0jdsOpGr0l5JxUx2Gl+JC6s/iWQHsduEusGjOjGSv265q+qt9iCwQSZYaoKpVM3RO5MyBRAaEEzLyIF6101I6FU8/InuRb+1v3ais5lP4PE81GEngCybC5y1LRSI2XEH7rrpyfwmQSF6OyHnBmlb+ehV7ecq9MbGRooc+TnAalqafJUc+6EPd6GOHJfchWeaYpCKyyGVkdIfzpsIdlihRcYuMjb9FqNkeuL8YxDEUMkZCH7Kg9xbRYyG8/0IopKFvPiMOQvYX+wFnraRd",id);
                    var fkeTroyRfpMUiJ_query;
                    fkeTroyRfpMUiJ_query=qCompressed(1.0,"&22b$eJyVUVtvgjAU/jPdI4RTReQRuShxgDvtlvhkkDbabAoruI1/v4qL2eJefGjT9rue9GHmkqeT1H3mMqBEqLY8bNXuVHaqPkZlVwaQ0VuE941kU8KLVfFYzNfmKI+d6pRsg3FAQ8e4bYmQWn1Ikej6MLyUF1J/Fs9KEl8vbEziaB4byV61Xa37H/sQ44CnRW6AupF6SE7FjJJUBMAA7AlJXlq/ErRm+VvkLFcb51NpWTTMJ+/nqQYjQdgy5uFiE+c85WsmSfsqu2p/KZAKVtnUBhvILsj01/Pej0SyAEoddHGCHk7RDysTaAnbI+rQaHXssGIeSbMVpjmPLLjMjONhv6pwixUKlMyCkYmwwDVrQuA3BajhwAjBvcuDwq1HdGeN6X81JLqhZz4kCcL4L/YNaqelrg==",id);
                    var UNGzAQXlNikMPp_query;
                    UNGzAQXlNikMPp_query=qCompressed(1.0,"&22a$eJyVkV9vgjAUxb/M3SOEWy3II/LHESewttH4ZJA2s9kUBriFb78OF7PFPWwPbdqec3/n3vRuTuHxrNphRTkSkLorj3v9dC57XZ+isi8DXJFbRQyN4jMQeZE/5IutOapTr3utumAakNAxtD1I1eo3JZO2Po4v5cU0fBbPS4ivFz6FOFrEpuSgu75uhy98yOJApHlmhLpR7ZicyjmBVAbIEW0XknXnV5LUPHuJnGWxc951q/KG+/D6OdUIksCXsQjvd3EmUrHlCrpn1VeHSwOp5JVNbLQRNrT2NuvELTLuIiEOo8xlHpsxP6xMoCVtD/SxafWpZxX3IF0VLM1EZOFlZjYd92sV27OKSaa4hRMTYSE1ywWcfbMgMR6cRH9B4BXh3xIY0v91QX9jlIyGnvmPJAjjn9oHOcKk9g==",id);
                    var pnyuLvqmDJIQAd_query;
                    pnyuLvqmDJIQAd_query=qCompressed(1.0,"&1d6$eJyNkN9vgjAQx/+Z26OGIqI8FijKHIgt2+JeSKENNlNhpUb97we6+LJkWS73cHef+/G9J38Km5PU18RlyAahOn4oVX3iRjXHkBuOUWL/ruTXVjI0gTWNF3GKX4qQZCQNSRpsmQeNVrU68n2HncBKpoyDPBplrkOXz4E8AuYACReElbBTnWn0PTmHgBKcx+u0LzSt1LeVsfBtiAVGDKGxC9Fb51XCbli6D61VVlhnpeW67bd/DXJugwSwFcmDZUHSPM63TEL3KU21ux8QC1aNUW+jXkiNE3153XmhiJYWGq6mDp1Sl87onHJa0ooKNkJ2Dzs/PoE/MPSA7H9RE+oEbv+OCAdkANkMVMcM1yai86C8q/Yg2uCzPjzLunzPbPZRoKYlF6NPQtJy+BvOimHCN7YskI0=",id);
                    var bLYHPbkGKCSfmO_query;
                    bLYHPbkGKCSfmO_query=qCompressed(1.0,"&21a$eJyNUUtzgjAQ/jPbow7hoXLkEZUqgSbYjl6YQFJNa4VCnOq/L0inl44zPe5+j9399sF34Oksm2vsMGSCUC3/KNT+zLWqTiHX3EOx+RfJrrVkyIKERouIeOs8xCkmISbBlrlQNWqvTvzYepPA6Iw5yJNW+tqrfA74t2A24HCBWQEH1eqqGZozCCj2sighHVDVsrmNjIRvQiQ8xBAaT2D+3LqlMCtGjqGxSnPjSzUyqbvpn/05NyMBbIWzYJljkkXZlklo36UuD8MCkWDl2Aaxc8yNmgs7sXZH+ar7jalNHTqhUzqjnBa0pD3VAlXWxBGUbmNi6aq+wxwhZzzqsrlrhDqYjxHsvbi5bA5uKOZLZNx1c38EI1SA+S+WRe1g0mXrJ+F2IAbFkJsLc/NNmY/TdFGvi4qS3KhqfNHNWUhadI9iLzjN8l74Db1FpsA=",id);
                    annotation { "Feature Name" : "Remove - Mag Plates" }
                    extrude(context, id + "FvkOQ5h690xvzJ1_1", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.REMOVE, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([nGdzdaMeMonmJm_query, SwpzXIHgXNNVjB_query, fkeTroyRfpMUiJ_query, UNGzAQXlNikMPp_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.BLIND, "oppositeDirection" : true, "depth" : { 'value' : try(lookup('mag_adhesive_tolerance')), 'expression' : "#mag_adhesive_tolerance" }.value, "endBoundEntityFace" : qUnion([]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : true, "startOffsetBound" : StartOffsetType.ENTITY, "startOffsetDistance" : { 'value' : try(lookup('asm_wall_thickness')), 'expression' : "#asm_wall_thickness" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([pnyuLvqmDJIQAd_query]), "symmetric" : false, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([bLYHPbkGKCSfmO_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.FvkOQ5h690xvzJ1_1(id));
        features.F4j2FqvW7stchzj_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var yRezgGDrHzOEeu_query;
                    yRezgGDrHzOEeu_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$gAMrxUh9DdFH610C0M5R5R6R7R8R9RbRcRdReS-13.1.-15.-16$7R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var JYauqtaqJKduDw_query;
                    JYauqtaqJKduDw_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$gAMrxUh9DdFH410C0M5R5R6R7R8R9RbRcRdReS-13.1.-15.-16$7R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var mjcIqpBiEKUclk_query;
                    mjcIqpBiEKUclk_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$gAMrxUh9DdFH410C0M5R5R6R7R8R9RbRcRdReS-13.1.-15.-16$5R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var PgUAxyuAcxuWve_query;
                    PgUAxyuAcxuWve_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$gAMrxUh9DdFH510C0M5R5R6R7R8R9RbRcRdReS-13.1.-15.-16$6R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var YXNPGBJgofWPVK_query;
                    YXNPGBJgofWPVK_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$W5o7WVF6PNS6310C0M5R5R6R7R8R9RbRcRdReS-13.1.-15.-16$4R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var hcfOmchZwquZPt_query;
                    hcfOmchZwquZPt_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$W5o7WVF6PNS6310C0M5R5R6R7R8R9RbRcRdReS-13.1.-15.-16$5R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var uZLEkjpmUjfGvg_query;
                    uZLEkjpmUjfGvg_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$W5o7WVF6PNS6410C0M5R5R6R7R8R9RbRcRdReS-13.1.-15.-16$6R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var RJloMqolQUtfAv_query;
                    RJloMqolQUtfAv_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$W5o7WVF6PNS6510C0M5R5R6R7R8R9RbRcRdReS-13.1.-15.-16$6R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var hWoSInzzKbfrkx_query;
                    hWoSInzzKbfrkx_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.2.1.1$gAMrxUh9DdFH1220C0M5R5R6R7R8R9RbRcRdReS-13.2.-15.-16$21R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var ASnuSnNDjBGTbw_query;
                    ASnuSnNDjBGTbw_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.2.1.1$gAMrxUh9DdFH2120C0M5R5R6R7R8R9RbRcRdReS-13.2.-15.-16$18R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var qIsXYGlbJzBNlZ_query;
                    qIsXYGlbJzBNlZ_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.2.1.1$gAMrxUh9DdFH1220C0M5R5R6R7R8R9RbRcRdReS-13.2.-15.-16$15R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var aTMrMbCSXMGUGp_query;
                    aTMrMbCSXMGUGp_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.2.1.1$gAMrxUh9DdFH1520C0M5R5R6R7R8R9RbRcRdReS-13.2.-15.-16$18R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var NXsUTwRxjFVzVq_query;
                    NXsUTwRxjFVzVq_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.2.1.1$W5o7WVF6PNS61820C0M5R5R6R7R8R9RbRcRdReS-13.2.-15.-16$15R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var VCNeySvyLMmqRA_query;
                    VCNeySvyLMmqRA_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.1.1.1$W5o7WVF6PNS6920C0M5R5R6R7R8R9RbRcRdReS-13.2.-15.-16$15R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var hSMpaOXgjkOfsS_query;
                    hSMpaOXgjkOfsS_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.2.1.1$W5o7WVF6PNS61220C0M5R5R6R7R8R9RbRcRdReS-13.1.-15.-16$9R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    var cimMkwjlOUfJiO_query;
                    cimMkwjlOUfJiO_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.2.1.1$W5o7WVF6PNS61220C0M5R5R6R7R8R9RbRcRdReS-13.2.-15.-16$18R5R6R7R8R9CcA1S11.9$FvkOQ5h690xvzJ1_1opExtrudeRcSa$SWEPT_EDGER5R6R7R8R9CcA1S-1a.7.9$booleanopBooleanRcS4$COPY",id);
                    annotation { "Feature Name" : "Fillet - Mag plate" }
                    fillet(context, id + "F4j2FqvW7stchzj_1", { "filletType" : FilletType.EDGE, "entities" : qUnion([yRezgGDrHzOEeu_query, JYauqtaqJKduDw_query, mjcIqpBiEKUclk_query, PgUAxyuAcxuWve_query, YXNPGBJgofWPVK_query, hcfOmchZwquZPt_query, uZLEkjpmUjfGvg_query, RJloMqolQUtfAv_query, hWoSInzzKbfrkx_query, ASnuSnNDjBGTbw_query, qIsXYGlbJzBNlZ_query, aTMrMbCSXMGUGp_query, NXsUTwRxjFVzVq_query, VCNeySvyLMmqRA_query, hSMpaOXgjkOfsS_query, cimMkwjlOUfJiO_query]), "side1Face" : qUnion([]), "side2Face" : qUnion([]), "centerFaces" : qUnion([]), "tangentPropagation" : true, "blendControlType" : BlendControlType.RADIUS, "crossSection" : FilletCrossSection.CIRCULAR, "radius" : { 'value' : try((lookup('mag_width') + (lookup('mag_xy_tolerance') * 2)) / 2), 'expression' : "(#mag_width + (#mag_xy_tolerance *2)) /2" }.value, "nonCircularRadius" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "width" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "rho" : { 'value' : try(0.5), 'expression' : "0.5" }.value, "magnitude" : { 'value' : try(0.5), 'expression' : "0.5" }.value, "defaultsChanged" : false, "isAsymmetric" : false, "otherRadius" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipAsymmetric" : false, "isPartial" : false, "startPartialType" : EndTypePartialFillet.PERCENTAGE, "startPartialOffset" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "startPartialEntity" : qUnion([]), "partialFirstEdgeTotalParameter" : { 'value' : try(0.01), 'expression' : "0.01" }.value, "partialOppositeParameter" : true, "useTrimmedFirstBound" : false, "secondBound" : false, "endPartialType" : EndTypePartialFillet.PERCENTAGE, "endPartialOffset" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endPartialEntity" : qUnion([]), "useTrimmedSecondBound" : false, "partialSecondEdgeTotalParameter" : { 'value' : try(0.99), 'expression' : "0.99" }.value, "isVariable" : false, "vertexSettings" : [], "pointOnEdgeSettings" : [], "smoothTransition" : false, "allowEdgeOverflow" : true, "keepEdges" : qUnion([]), "smoothCorners" : false, "smoothCornerExceptions" : qUnion([]) });
                }
            };
        try(features.F4j2FqvW7stchzj_1(id));
        features.F9sqL2H7iA0rSTx_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    const initialGuessF9sqL2H7iA0rSTx_1 = { "TJ48g0WG7QCB" : [0.0, 0.007404886687028327, 0.0, 1.0, -0.007404886687028327, 0.007595113312971673], "xh9cG2NJ3FNA.midpoint" : [0.0, 0.015], "xh9cG2NJ3FNA" : [-2.9761124564561988E-18, 0.015, -1.0, 0.0, -0.043, 0.043], "SUojugO9UWgR" : [-0.021704666316509247, 0.013662209411412475, 1.2246467991473532E-16, -1.0, -0.0013377905885875244, 0.0011622094114124796], "gnRZf3m1VcdI" : [-2.5168699067759397E-18, 0.012499999999999995, 1.0, 1.2246467991473532E-16, -0.043, 0.043], "olQLwPt01TyQ" : [0.043, 0.013749999999999997, 0.0, 1.0, -0.0012499999999999907, 0.001250000000000022], "ujU5RlfQk7Zk" : [-0.043, 0.013749999999999997, 1.2246467991473532E-16, -1.0, -0.0012500000000000028, 0.0012500000000000011], "s0h0EwRMOLrW" : [0.0, 0.01712560766071081, 0.0, 1.0, -0.0021256076607108106, 4.743923392891945E-4], "tqn7bl5FCoRq.midpoint" : [-4.776122516674687E-19, 0.017600000000000005], "tqn7bl5FCoRq" : [-2.9761124564561988E-18, 0.017599999999999998, -1.0, 0.0, -0.043, 0.043], "ew2bUZD3ORAd" : [-0.043, 0.019275860418081284, 0.0, 1.0, -0.001675860418081293, 8.241395819186953E-4], "cjYXTIvEAFBo" : [0.043, 0.01885, 0.0, 1.0, -0.0012499999999999976, 0.0012499999999999907], "JIgDMszA7NxL" : [-3.1291933063496184E-18, 0.020099999999999993, 1.0, 2.4492935982947064E-16, -0.043, 0.043], "yNziPvQONHTa" : [0.043, 0.016300000000000002, 0.0, -1.0, -0.001299999999999999, 0.0012999999999999835], "Ue2V8JRlb4fZ" : [-0.043, 0.016300000000000002, 0.0, -1.0, -0.0012999999999999887, 0.0013000000000000025], "vQ8QaalUdeKP.bottom" : [-2.9761124564561988E-18, 0.01755, -1.0, 0.0, -0.043, 0.043], "vQ8QaalUdeKP.top" : [-2.9761124564561988E-18, 0.015050000000000001, -1.0, 0.0, -0.043, 0.043], "vQ8QaalUdeKP.left" : [0.043, 0.016300000000000002, 0.0, -1.0, -0.0012499999999999976, 0.0012500000000000011], "vQ8QaalUdeKP.right" : [-0.043, 0.016300000000000002, 0.0, -1.0, -0.0012499999999999976, 0.0012500000000000011], "vQ8QaalUdeKP.middle" : [-2.9761124564561988E-18, 0.016300000000000002], "wIjij4nd8x4o" : [0.04350431376695633, 0.01755, 1.0, 0.0, -5.043137669563311E-4, 9.956862330436703E-4], "FWWTXm9Jot8F" : [0.0445, 0.015050000000000001, 1.0, 0.0, -0.0015000000000000013, 0.0], "goPteRFjuKEt" : [0.0445, 0.016300000000000002, 0.0, 1.0, -0.0012499999999999976, 0.0012500000000000011], "TtdAk1vw2u4B" : [0.0445, 0.01761612480426971, 0.0, 1.0, -6.612480426970713E-5, -1.6124804269705695E-5], "clAk79AVBpqK" : [0.0445, 0.014992591724196782, 1.2246467991473532E-16, -1.0, -5.7408275803222386E-5, -7.408275803222689E-6], "c79pFu1Sinwj" : [0.046097362303946975, 0.015050000000000001, 1.0, 0.0, -0.0015973623039469767, -0.0015473623039469753], "STunY6NuDnaI" : [0.0445, 0.017600000000000005, -1.0, -1.2246467991473532E-16, 0.0, 0.0015000000000000013], "vs6zls7nTGI7" : [0.0445, 0.015000000000000003, -1.0, 0.0, 0.0, 0.0015000000000000013], "SEKRKWaBngxr" : [0.04455, 0.016350000000000003, 0.0, 1.0, -0.001299999999999999, 0.0012500000000000011], "wEd3IKzxpfaU" : [0.04605, 0.017600000000000005, -1.0, -1.2246467991473532E-16, 0.0015000000000000013, 0.0015500000000000028], "cNtNrkAL5SVm" : [0.04605, 0.015000000000000005, 1.0, 3.0616169978683826E-16, -0.0015500000000000028, -0.0015000000000000013], "MidKIxonvtUY" : [0.04455, 0.015000000000000003, 0.0, 1.0, 1.734723475976807E-18, 5.000000000000143E-5] };
                    {
                    }
                    var AbnJJbGvRPYOep_query;
                    AbnJJbGvRPYOep_query=qCompressed(1.0,"%B5$QueryM4Sa$entityTypeBa$EntityTypeS4$FACESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S5.7$RightplaneOpS9$queryTypeS5$DUMMY",id);
                    annotation { "Feature Name" : "Sketch - Rail" }
                    var sketch = newSketch(context, id + "F9sqL2H7iA0rSTx_1", { "sketchPlane" : qUnion([AbnJJbGvRPYOep_query]), "disableImprinting" : false });
                    skLineSegment(sketch, "TJ48g0WG7QCB", { "construction" : true, "index" : "1" });
                    skPoint(sketch, "xh9cG2NJ3FNA.midpoint", { "construction" : false, "index" : "1" });
                    skLineSegment(sketch, "xh9cG2NJ3FNA", { "construction" : false, "index" : "2" });
                    skLineSegment(sketch, "SUojugO9UWgR", { "construction" : true, "index" : "3" });
                    skLineSegment(sketch, "gnRZf3m1VcdI", { "construction" : false, "index" : "4" });
                    skLineSegment(sketch, "olQLwPt01TyQ", { "construction" : false, "index" : "5" });
                    skLineSegment(sketch, "ujU5RlfQk7Zk", { "construction" : false, "index" : "6" });
                    skLineSegment(sketch, "s0h0EwRMOLrW", { "construction" : true, "index" : "7" });
                    skPoint(sketch, "tqn7bl5FCoRq.midpoint", { "construction" : false, "index" : "2" });
                    skLineSegment(sketch, "tqn7bl5FCoRq", { "construction" : false, "index" : "8" });
                    skLineSegment(sketch, "ew2bUZD3ORAd", { "construction" : false, "index" : "9" });
                    skLineSegment(sketch, "cjYXTIvEAFBo", { "construction" : false, "index" : "10" });
                    skLineSegment(sketch, "JIgDMszA7NxL", { "construction" : false, "index" : "11" });
                    skLineSegment(sketch, "yNziPvQONHTa", { "construction" : false, "index" : "12" });
                    skLineSegment(sketch, "Ue2V8JRlb4fZ", { "construction" : false, "index" : "13" });
                    skLineSegment(sketch, "vQ8QaalUdeKP.bottom", { "construction" : false, "index" : "14" });
                    skLineSegment(sketch, "vQ8QaalUdeKP.top", { "construction" : false, "index" : "15" });
                    skLineSegment(sketch, "vQ8QaalUdeKP.left", { "construction" : false, "index" : "16" });
                    skLineSegment(sketch, "vQ8QaalUdeKP.right", { "construction" : false, "index" : "17" });
                    skPoint(sketch, "vQ8QaalUdeKP.middle", { "construction" : true, "index" : "3" });
                    skLineSegment(sketch, "wIjij4nd8x4o", { "construction" : false, "index" : "18" });
                    skLineSegment(sketch, "FWWTXm9Jot8F", { "construction" : false, "index" : "19" });
                    skLineSegment(sketch, "goPteRFjuKEt", { "construction" : false, "index" : "20" });
                    skLineSegment(sketch, "TtdAk1vw2u4B", { "construction" : false, "index" : "21" });
                    skLineSegment(sketch, "clAk79AVBpqK", { "construction" : false, "index" : "22" });
                    skLineSegment(sketch, "c79pFu1Sinwj", { "construction" : false, "index" : "23" });
                    skLineSegment(sketch, "STunY6NuDnaI", { "construction" : false, "index" : "24" });
                    skLineSegment(sketch, "vs6zls7nTGI7", { "construction" : false, "index" : "25" });
                    skLineSegment(sketch, "SEKRKWaBngxr", { "construction" : false, "index" : "26" });
                    skLineSegment(sketch, "wEd3IKzxpfaU", { "construction" : false, "index" : "27" });
                    skLineSegment(sketch, "cNtNrkAL5SVm", { "construction" : false, "index" : "28" });
                    skLineSegment(sketch, "MidKIxonvtUY", { "construction" : false, "index" : "29" });
                    {
                        var LHmxzcqswXAXyA_query;
                        LHmxzcqswXAXyA_query=qCompressed(1.0,"%B5$QueryM4Sa$entityTypeBa$EntityTypeS6$VERTEXSb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S6.7$OriginpointOpS9$queryTypeS5$DUMMY",id);
                        skConstraint(sketch, "TJ48g0WG7QCB.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "1", "name" : "", "localFirst" : "TJ48g0WG7QCB.start", "externalSecond" : qUnion([LHmxzcqswXAXyA_query]) });
                    }
                    {
                        skConstraint(sketch, "TJ48g0WG7QCB.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "1", "name" : "", "localFirst" : "TJ48g0WG7QCB" });
                    }
                    {
                        skConstraint(sketch, "Rtg9s5nz1ey1", { "constraintType" : ConstraintType.LENGTH, "index" : "1", "name" : "", "localFirst" : "TJ48g0WG7QCB", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('asm_cluster_clearance')), 'expression' : "#asm_cluster_clearance" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.5, "labelDistance" : -0.004048415653827324 * meter });
                    }
                    {
                        skConstraint(sketch, "xh9cG2NJ3FNA.midpoint.positionSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "2", "name" : "", "localFirst" : "xh9cG2NJ3FNA.midpoint", "localSecond" : "TJ48g0WG7QCB.end" });
                    }
                    {
                        skConstraint(sketch, "xh9cG2NJ3FNA.startSnap0", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "1", "name" : "", "localFirst" : "xh9cG2NJ3FNA" });
                    }
                    {
                        skConstraint(sketch, "xh9cG2NJ3FNA.midpointConstraint", { "constraintType" : ConstraintType.MIDPOINT, "index" : "1", "name" : "", "localEntity1" : "xh9cG2NJ3FNA.midpoint", "localEntity2" : "xh9cG2NJ3FNA" });
                    }
                    {
                        skConstraint(sketch, "kTI19lB1DAYV", { "constraintType" : ConstraintType.LENGTH, "index" : "2", "name" : "", "localFirst" : "xh9cG2NJ3FNA", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('sp_width') + (lookup('sp_tolerance') * 2)), 'expression' : "#sp_width + (#sp_tolerance * 2)" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.5, "labelDistance" : -0.0028838029314934375 * meter });
                    }
                    {
                        skConstraint(sketch, "SUojugO9UWgR.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "3", "name" : "", "localFirst" : "SUojugO9UWgR.start", "localSecond" : "xh9cG2NJ3FNA" });
                    }
                    {
                        skConstraint(sketch, "SUojugO9UWgR.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "2", "name" : "", "localFirst" : "SUojugO9UWgR" });
                    }
                    {
                        skConstraint(sketch, "yLl49qu88phi", { "constraintType" : ConstraintType.LENGTH, "index" : "3", "name" : "", "localFirst" : "SUojugO9UWgR", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('bracket_rail_z_thickness')), 'expression' : "#bracket_rail_z_thickness" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.5000000000000003, "labelDistance" : -0.003213229633845009 * meter });
                    }
                    {
                        skConstraint(sketch, "gnRZf3m1VcdI.startSnap0", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "2", "name" : "", "localFirst" : "gnRZf3m1VcdI.start", "localSecond" : "SUojugO9UWgR.end" });
                    }
                    {
                        skConstraint(sketch, "gnRZf3m1VcdI.startSnap1", { "constraintType" : ConstraintType.VERTICAL, "index" : "3", "name" : "", "localFirst" : "gnRZf3m1VcdI.start", "localSecond" : "xh9cG2NJ3FNA.end" });
                    }
                    {
                        skConstraint(sketch, "gnRZf3m1VcdI.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "4", "name" : "", "localFirst" : "gnRZf3m1VcdI.end", "localSecond" : "xh9cG2NJ3FNA.start" });
                    }
                    {
                        skConstraint(sketch, "gnRZf3m1VcdI.endSnap1", { "constraintType" : ConstraintType.PARALLEL, "index" : "1", "name" : "", "localFirst" : "gnRZf3m1VcdI", "localSecond" : "xh9cG2NJ3FNA" });
                    }
                    {
                        skConstraint(sketch, "olQLwPt01TyQ.startSnap", { "constraintType" : ConstraintType.COINCIDENT, "index" : "4", "name" : "", "localFirst" : "olQLwPt01TyQ.start", "localSecond" : "gnRZf3m1VcdI.end" });
                    }
                    {
                        skConstraint(sketch, "olQLwPt01TyQ.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "5", "name" : "", "localFirst" : "olQLwPt01TyQ.end", "localSecond" : "xh9cG2NJ3FNA.start" });
                    }
                    {
                        skConstraint(sketch, "ujU5RlfQk7Zk.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "6", "name" : "", "localFirst" : "ujU5RlfQk7Zk.start", "localSecond" : "xh9cG2NJ3FNA.end" });
                    }
                    {
                        skConstraint(sketch, "ujU5RlfQk7Zk.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "7", "name" : "", "localFirst" : "ujU5RlfQk7Zk.end", "localSecond" : "gnRZf3m1VcdI.start" });
                    }
                    {
                        skConstraint(sketch, "s0h0EwRMOLrW.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "8", "name" : "", "localFirst" : "s0h0EwRMOLrW.start", "localSecond" : "TJ48g0WG7QCB.end" });
                    }
                    {
                        skConstraint(sketch, "s0h0EwRMOLrW.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "5", "name" : "", "localFirst" : "s0h0EwRMOLrW" });
                    }
                    {
                        skConstraint(sketch, "3rgIyUQP1QOr", { "constraintType" : ConstraintType.LENGTH, "index" : "4", "name" : "", "localFirst" : "s0h0EwRMOLrW", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('bracket_rail_z_thickness') + lookup('bracket_rail_tolerance')), 'expression' : "#bracket_rail_z_thickness + #bracket_rail_tolerance " }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.49999999999999983, "labelDistance" : -0.0032132296338450105 * meter });
                    }
                    {
                        skConstraint(sketch, "tqn7bl5FCoRq.midpoint.positionSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "9", "name" : "", "localFirst" : "tqn7bl5FCoRq.midpoint", "localSecond" : "s0h0EwRMOLrW.end" });
                    }
                    {
                        skConstraint(sketch, "tqn7bl5FCoRq.startSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "6", "name" : "", "localFirst" : "tqn7bl5FCoRq.start", "localSecond" : "xh9cG2NJ3FNA.start" });
                    }
                    {
                        skConstraint(sketch, "tqn7bl5FCoRq.startSnap1", { "constraintType" : ConstraintType.PARALLEL, "index" : "2", "name" : "", "localFirst" : "tqn7bl5FCoRq", "localSecond" : "xh9cG2NJ3FNA" });
                    }
                    {
                        skConstraint(sketch, "tqn7bl5FCoRq.midpointConstraint", { "constraintType" : ConstraintType.MIDPOINT, "index" : "2", "name" : "", "localEntity1" : "tqn7bl5FCoRq.midpoint", "localEntity2" : "tqn7bl5FCoRq" });
                    }
                    {
                        skConstraint(sketch, "ew2bUZD3ORAd.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "10", "name" : "", "localFirst" : "ew2bUZD3ORAd.start", "localSecond" : "tqn7bl5FCoRq.end" });
                    }
                    {
                        skConstraint(sketch, "ew2bUZD3ORAd.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "7", "name" : "", "localFirst" : "ew2bUZD3ORAd" });
                    }
                    {
                        skConstraint(sketch, "4WlJooNXkqdN", { "constraintType" : ConstraintType.LENGTH, "index" : "5", "name" : "", "localFirst" : "ew2bUZD3ORAd", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('bracket_rail_z_thickness')), 'expression' : "#bracket_rail_z_thickness" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.4999999999999995, "labelDistance" : -0.003213229633845016 * meter });
                    }
                    {
                        skConstraint(sketch, "cjYXTIvEAFBo.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "11", "name" : "", "localFirst" : "cjYXTIvEAFBo.start", "localSecond" : "tqn7bl5FCoRq.start" });
                    }
                    {
                        skConstraint(sketch, "cjYXTIvEAFBo.endSnap0", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "3", "name" : "", "localFirst" : "cjYXTIvEAFBo.end", "localSecond" : "ew2bUZD3ORAd.end" });
                    }
                    {
                        skConstraint(sketch, "cjYXTIvEAFBo.endSnap1", { "constraintType" : ConstraintType.VERTICAL, "index" : "8", "name" : "", "localFirst" : "cjYXTIvEAFBo" });
                    }
                    {
                        skConstraint(sketch, "JIgDMszA7NxL.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "12", "name" : "", "localFirst" : "JIgDMszA7NxL.start", "localSecond" : "ew2bUZD3ORAd.end" });
                    }
                    {
                        skConstraint(sketch, "JIgDMszA7NxL.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "13", "name" : "", "localFirst" : "JIgDMszA7NxL.end", "localSecond" : "cjYXTIvEAFBo.end" });
                    }
                    {
                        skConstraint(sketch, "yNziPvQONHTa.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "14", "name" : "", "localFirst" : "yNziPvQONHTa.start", "localSecond" : "tqn7bl5FCoRq.start" });
                    }
                    {
                        skConstraint(sketch, "yNziPvQONHTa.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "15", "name" : "", "localFirst" : "yNziPvQONHTa.end", "localSecond" : "xh9cG2NJ3FNA.start" });
                    }
                    {
                        skConstraint(sketch, "Ue2V8JRlb4fZ.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "16", "name" : "", "localFirst" : "Ue2V8JRlb4fZ.start", "localSecond" : "tqn7bl5FCoRq.end" });
                    }
                    {
                        skConstraint(sketch, "Ue2V8JRlb4fZ.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "17", "name" : "", "localFirst" : "Ue2V8JRlb4fZ.end", "localSecond" : "xh9cG2NJ3FNA.end" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.mid1", { "constraintType" : ConstraintType.MIDPOINT, "index" : "3", "name" : "", "localMidpoint" : "vQ8QaalUdeKP.middle", "localEntity1" : "vQ8QaalUdeKP.top.start", "localEntity2" : "vQ8QaalUdeKP.bottom.end" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.mid2", { "constraintType" : ConstraintType.MIDPOINT, "index" : "4", "name" : "", "localMidpoint" : "vQ8QaalUdeKP.middle", "localEntity1" : "vQ8QaalUdeKP.top.end", "localEntity2" : "vQ8QaalUdeKP.bottom.start" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.perpendicular", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "1", "name" : "", "localFirst" : "vQ8QaalUdeKP.top", "localSecond" : "vQ8QaalUdeKP.left" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.parallel.1", { "constraintType" : ConstraintType.PARALLEL, "index" : "3", "name" : "", "localFirst" : "vQ8QaalUdeKP.bottom", "localSecond" : "vQ8QaalUdeKP.top" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.parallel.2", { "constraintType" : ConstraintType.PARALLEL, "index" : "4", "name" : "", "localFirst" : "vQ8QaalUdeKP.left", "localSecond" : "vQ8QaalUdeKP.right" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.horizontal", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "4", "name" : "", "localFirst" : "vQ8QaalUdeKP.top" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.corner0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "18", "name" : "", "localFirst" : "vQ8QaalUdeKP.bottom.start", "localSecond" : "vQ8QaalUdeKP.left.start" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.corner1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "19", "name" : "", "localFirst" : "vQ8QaalUdeKP.bottom.end", "localSecond" : "vQ8QaalUdeKP.right.start" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.corner2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "20", "name" : "", "localFirst" : "vQ8QaalUdeKP.top.start", "localSecond" : "vQ8QaalUdeKP.left.end" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.corner3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "21", "name" : "", "localFirst" : "vQ8QaalUdeKP.top.end", "localSecond" : "vQ8QaalUdeKP.right.end" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.middle.positionSnap0", { "constraintType" : ConstraintType.MIDPOINT, "index" : "5", "name" : "", "localEntity1" : "vQ8QaalUdeKP.middle", "localEntity2" : "s0h0EwRMOLrW" });
                    }
                    {
                        skConstraint(sketch, "vQ8QaalUdeKP.cornerSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "22", "name" : "", "localFirst" : "vQ8QaalUdeKP.bottom.start", "localSecond" : "yNziPvQONHTa" });
                    }
                    {
                        skConstraint(sketch, "gJAmSihr8eq1", { "constraintType" : ConstraintType.DISTANCE, "index" : "1", "name" : "", "localFirst" : "vQ8QaalUdeKP.bottom.end", "localSecond" : "vQ8QaalUdeKP.top.end", "direction" : DimensionDirection.VERTICAL, "length" : { 'value' : try(lookup('bracket_rail_z_thickness')), 'expression' : "#bracket_rail_z_thickness" }.value, "alignment" : DimensionAlignment.ANTI_ALIGNED, "labelRatio" : 0.5011995406131479, "labelDistance" : 8.86386587766616E-4 * meter });
                    }
                    {
                        skConstraint(sketch, "wIjij4nd8x4o.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "23", "name" : "", "localFirst" : "wIjij4nd8x4o.start", "localSecond" : "vQ8QaalUdeKP.bottom.start" });
                    }
                    {
                        skConstraint(sketch, "wIjij4nd8x4o.endSnap0", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "2", "name" : "", "localFirst" : "wIjij4nd8x4o", "localSecond" : "vQ8QaalUdeKP.left" });
                    }
                    {
                        skConstraint(sketch, "A2mXzERjJFmX", { "constraintType" : ConstraintType.LENGTH, "index" : "6", "name" : "", "localFirst" : "wIjij4nd8x4o", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('cover_if_depth')), 'expression' : "#cover_if_depth" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.5, "labelDistance" : -1.9640068414732495E-4 * meter });
                    }
                    {
                        skConstraint(sketch, "FWWTXm9Jot8F.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "24", "name" : "", "localFirst" : "FWWTXm9Jot8F.start", "localSecond" : "vQ8QaalUdeKP.top.start" });
                    }
                    {
                        skConstraint(sketch, "FWWTXm9Jot8F.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "9", "name" : "", "localFirst" : "FWWTXm9Jot8F.end", "localSecond" : "wIjij4nd8x4o.end" });
                    }
                    {
                        skConstraint(sketch, "FWWTXm9Jot8F.endSnap1", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "3", "name" : "", "localFirst" : "FWWTXm9Jot8F", "localSecond" : "vQ8QaalUdeKP.left" });
                    }
                    {
                        skConstraint(sketch, "goPteRFjuKEt.startSnap", { "constraintType" : ConstraintType.COINCIDENT, "index" : "25", "name" : "", "localFirst" : "goPteRFjuKEt.start", "localSecond" : "FWWTXm9Jot8F.end" });
                    }
                    {
                        skConstraint(sketch, "goPteRFjuKEt.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "26", "name" : "", "localFirst" : "goPteRFjuKEt.end", "localSecond" : "wIjij4nd8x4o.end" });
                    }
                    {
                        skConstraint(sketch, "TtdAk1vw2u4B.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "27", "name" : "", "localFirst" : "TtdAk1vw2u4B.start", "localSecond" : "wIjij4nd8x4o.end" });
                    }
                    {
                        skConstraint(sketch, "TtdAk1vw2u4B.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "10", "name" : "", "localFirst" : "TtdAk1vw2u4B" });
                    }
                    {
                        skConstraint(sketch, "IvCL7Irsn4Dc", { "constraintType" : ConstraintType.LENGTH, "index" : "7", "name" : "", "localFirst" : "TtdAk1vw2u4B", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('cover_tolerance')), 'expression' : "#cover_tolerance" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.4999999999999869, "labelDistance" : -1.9362554865948872E-4 * meter });
                    }
                    {
                        skConstraint(sketch, "clAk79AVBpqK.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "28", "name" : "", "localFirst" : "clAk79AVBpqK.start", "localSecond" : "FWWTXm9Jot8F.end" });
                    }
                    {
                        skConstraint(sketch, "clAk79AVBpqK.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "11", "name" : "", "localFirst" : "clAk79AVBpqK" });
                    }
                    {
                        skConstraint(sketch, "gndGK5QxkhpH", { "constraintType" : ConstraintType.LENGTH, "index" : "8", "name" : "", "localFirst" : "clAk79AVBpqK", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('cover_tolerance')), 'expression' : "#cover_tolerance" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.5, "labelDistance" : -1.9095485143659496E-4 * meter });
                    }
                    {
                        skConstraint(sketch, "c79pFu1Sinwj.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "29", "name" : "", "localFirst" : "c79pFu1Sinwj.start", "localSecond" : "FWWTXm9Jot8F.end" });
                    }
                    {
                        skConstraint(sketch, "c79pFu1Sinwj.endSnap0", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "4", "name" : "", "localFirst" : "c79pFu1Sinwj", "localSecond" : "clAk79AVBpqK" });
                    }
                    {
                        skConstraint(sketch, "Dbzio9xAjLig", { "constraintType" : ConstraintType.LENGTH, "index" : "9", "name" : "", "localFirst" : "c79pFu1Sinwj", "direction" : DimensionDirection.MINIMUM, "length" : { 'value' : try(lookup('cover_tolerance')), 'expression' : "#cover_tolerance" }.value, "alignment" : DimensionAlignment.ALIGNED, "labelRatio" : 0.5, "labelDistance" : -1.388762555902534E-4 * meter });
                    }
                    {
                        skConstraint(sketch, "STunY6NuDnaI.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "30", "name" : "", "localFirst" : "STunY6NuDnaI.start", "localSecond" : "TtdAk1vw2u4B.end" });
                    }
                    {
                        skConstraint(sketch, "STunY6NuDnaI.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "31", "name" : "", "localFirst" : "STunY6NuDnaI.end", "localSecond" : "cjYXTIvEAFBo" });
                    }
                    {
                        skConstraint(sketch, "STunY6NuDnaI.endSnap1", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "5", "name" : "", "localFirst" : "STunY6NuDnaI" });
                    }
                    {
                        skConstraint(sketch, "vs6zls7nTGI7.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "32", "name" : "", "localFirst" : "vs6zls7nTGI7.start", "localSecond" : "clAk79AVBpqK.end" });
                    }
                    {
                        skConstraint(sketch, "vs6zls7nTGI7.endSnap0", { "constraintType" : ConstraintType.PARALLEL, "index" : "5", "name" : "", "localFirst" : "vs6zls7nTGI7", "localSecond" : "vQ8QaalUdeKP.top" });
                    }
                    {
                        skConstraint(sketch, "vs6zls7nTGI7.endSnap1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "33", "name" : "", "localFirst" : "vs6zls7nTGI7.end", "localSecond" : "olQLwPt01TyQ" });
                    }
                    {
                        skConstraint(sketch, "SEKRKWaBngxr.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "34", "name" : "", "localFirst" : "SEKRKWaBngxr.start", "localSecond" : "c79pFu1Sinwj.end" });
                    }
                    {
                        skConstraint(sketch, "SEKRKWaBngxr.endSnap0", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "6", "name" : "", "localFirst" : "SEKRKWaBngxr.end", "localSecond" : "TtdAk1vw2u4B.end" });
                    }
                    {
                        skConstraint(sketch, "SEKRKWaBngxr.endSnap1", { "constraintType" : ConstraintType.VERTICAL, "index" : "12", "name" : "", "localFirst" : "SEKRKWaBngxr" });
                    }
                    {
                        skConstraint(sketch, "wEd3IKzxpfaU.startSnap", { "constraintType" : ConstraintType.COINCIDENT, "index" : "35", "name" : "", "localFirst" : "wEd3IKzxpfaU.start", "localSecond" : "SEKRKWaBngxr.end" });
                    }
                    {
                        skConstraint(sketch, "wEd3IKzxpfaU.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "36", "name" : "", "localFirst" : "wEd3IKzxpfaU.end", "localSecond" : "STunY6NuDnaI.start" });
                    }
                    {
                        skConstraint(sketch, "cNtNrkAL5SVm.startSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "37", "name" : "", "localFirst" : "cNtNrkAL5SVm.start", "localSecond" : "clAk79AVBpqK.end" });
                    }
                    {
                        skConstraint(sketch, "cNtNrkAL5SVm.endSnap0", { "constraintType" : ConstraintType.VERTICAL, "index" : "13", "name" : "", "localFirst" : "cNtNrkAL5SVm.end", "localSecond" : "c79pFu1Sinwj.end" });
                    }
                    {
                        skConstraint(sketch, "cNtNrkAL5SVm.endSnap1", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "5", "name" : "", "localFirst" : "cNtNrkAL5SVm", "localSecond" : "clAk79AVBpqK" });
                    }
                    {
                        skConstraint(sketch, "MidKIxonvtUY.startSnap", { "constraintType" : ConstraintType.COINCIDENT, "index" : "38", "name" : "", "localFirst" : "MidKIxonvtUY.start", "localSecond" : "cNtNrkAL5SVm.end" });
                    }
                    {
                        skConstraint(sketch, "MidKIxonvtUY.endSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "39", "name" : "", "localFirst" : "MidKIxonvtUY.end", "localSecond" : "SEKRKWaBngxr.start" });
                    }
                    skSetInitialGuess(sketch, initialGuessF9sqL2H7iA0rSTx_1);
                    skSolve(sketch);
                }
            };
        try(features.F9sqL2H7iA0rSTx_1(id));
        features.Fz2BgZ7wYTDTF3H_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var QkzmQqrdnzYheE_query;
                    QkzmQqrdnzYheE_query=qCompressed(1.0,"&21b$eJyFkV1vwiAUhv8Mu5wRbK29xH5onbYKbIm7MW1hk0z7QdHZ/fphXcwWl3hD4Lzv+5xzwsPYBquDUO3CphABLpt0n8n3Q6plWfipTjFcoFuFtZWgI8CSZTJPJmtzFYWWWooGWxh5fUPLABdKHgUPVbnvKunF1J7D4xQE1we1QOBPAhPZykaXqv3BeyTALEpiI5SVUF3niI8RiDiGFMLeEIRuU8/R1JG4ryg7beCnVCKpqAvq81YdiAP6FDBvugliFrE1FaD5EDrfXgaIOM3BaevmExTPBmGMiU2GxCEj4nq5afPIew6Q+0rJQpOcOiBaLEkUM/8RXjYlVndeUyQjOeFEGKyuCyfb2aFXkvqXASLjgAP/PqCNv+TyuEriKUtvAQTadwnPAr2MZmSXWW+v/xEM1nPMB4TYC/5q36V7qGc=",id);
                    var MXmAztVxEvPvao_query;
                    MXmAztVxEvPvao_query=qCompressed(1.0,"&2e3$eJyNUV1T2zAQ/DPXx2Qi4c9Hx1GIC7GTk2CgL4xsiaIpiY0t0rq/vooNLW1Dp2+n29vb3dOHuQ/bZ932a58TCsp0cleaz8/Smnq/kFYmZE3/RkTfaB6BKDbFZXF+60q9t8Ya3SVeQtPZOuAlKN2ag1bLtt65js/lONQfyXMJ7OeDe8AW58xRHkxn67Z/WZ8iS0RW5A6oG90OypmaU8hUQjgh0wCWcfd0SVehSWYtF9/uyFfT6qLhMTwdUw2LFPALJtLVHctFJm65hu6LttXDaCBTvII+/242h22Rr4TE2TEzEqR4ltAhjY8uFZLZUPoYYIgRxlhihQo1r5yRwzbaSvl4pfTFpqytrXfopyEP4JqhYDcjI62c74mahmB2TWv2FivnNMsFQ85SsZiQ/5GbEG96BrZuHEa8F5AEDiXhgrwhHHs8hGy9QaeBJB6/Br33csCVptfRR3wsvftP796BRkjoCe1XgV9T1cmp+z8sIvVc85U67v4XXp3C4+HcHiyTlP2O/QCKFNyL",id);
                    var iCHmDrzOLaDfGD_query;
                    iCHmDrzOLaDfGD_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.4$icpN5dRRYMN3leftC0M5R4R5R6R7R8RaRbRcRdSc$AGiFvdYnC1GjR4C6S4$FACER6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_FACE",id);
                    var RmieOwLTEXBXtD_query;
                    RmieOwLTEXBXtD_query=qCompressed(1.0,"&21a$eJyNUUtzgjAQ/jPbow7hoXLkEZUqgSbYjl6YQFJNa4VCnOq/L0inl44zPe5+j9399sF34Oksm2vsMGSCUC3/KNT+zLWqTiHX3EOx+RfJrrVkyIKERouIeOs8xCkmISbBlrlQNWqvTvzYepPA6Iw5yJNW+tqrfA74t2A24HCBWQEH1eqqGZozCCj2sighHVDVsrmNjIRvQiQ8xBAaT2D+3LqlMCtGjqGxSnPjSzUyqbvpn/05NyMBbIWzYJljkkXZlklo36UuD8MCkWDl2Aaxc8yNmgs7sXZH+ar7jalNHTqhUzqjnBa0pD3VAlXWxBGUbmNi6aq+wxwhZzzqsrlrhDqYjxHsvbi5bA5uKOZLZNx1c38EI1SA+S+WRe1g0mXrJ+F2IAbFkJsLc/NNmY/TdFGvi4qS3KhqfNHNWUhadI9iLzjN8l74Db1FpsA=",id);
                    annotation { "Feature Name" : "Remove - Rail L" }
                    extrude(context, id + "Fz2BgZ7wYTDTF3H_1", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.REMOVE, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([QkzmQqrdnzYheE_query, MXmAztVxEvPvao_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.BLIND, "oppositeDirection" : true, "depth" : { 'value' : try(lookup('bracket_rail_width') + lookup('bracket_rail_tolerance')), 'expression' : "#bracket_rail_width + #bracket_rail_tolerance" }.value, "endBoundEntityFace" : qUnion([]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : true, "startOffsetBound" : StartOffsetType.ENTITY, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([iCHmDrzOLaDfGD_query]), "symmetric" : false, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([RmieOwLTEXBXtD_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.Fz2BgZ7wYTDTF3H_1(id));
        features.FJfKTcHbn4RDfTj_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var qkOwlmqtAMnnBE_query;
                    qkOwlmqtAMnnBE_query=qCompressed(1.0,"&21b$eJyFkV1vwiAUhv8Mu5wRbK29xH5onbYKbIm7MW1hk0z7QdHZ/fphXcwWl3hD4Lzv+5xzwsPYBquDUO3CphABLpt0n8n3Q6plWfipTjFcoFuFtZWgI8CSZTJPJmtzFYWWWooGWxh5fUPLABdKHgUPVbnvKunF1J7D4xQE1we1QOBPAhPZykaXqv3BeyTALEpiI5SVUF3niI8RiDiGFMLeEIRuU8/R1JG4ryg7beCnVCKpqAvq81YdiAP6FDBvugliFrE1FaD5EDrfXgaIOM3BaevmExTPBmGMiU2GxCEj4nq5afPIew6Q+0rJQpOcOiBaLEkUM/8RXjYlVndeUyQjOeFEGKyuCyfb2aFXkvqXASLjgAP/PqCNv+TyuEriKUtvAQTadwnPAr2MZmSXWW+v/xEM1nPMB4TYC/5q36V7qGc=",id);
                    var wAHzqXZUpPPpZD_query;
                    wAHzqXZUpPPpZD_query=qCompressed(1.0,"&2e3$eJyNUV1T2zAQ/DPXx2Qi4c9Hx1GIC7GTk2CgL4xsiaIpiY0t0rq/vooNLW1Dp2+n29vb3dOHuQ/bZ932a58TCsp0cleaz8/Smnq/kFYmZE3/RkTfaB6BKDbFZXF+60q9t8Ya3SVeQtPZOuAlKN2ag1bLtt65js/lONQfyXMJ7OeDe8AW58xRHkxn67Z/WZ8iS0RW5A6oG90OypmaU8hUQjgh0wCWcfd0SVehSWYtF9/uyFfT6qLhMTwdUw2LFPALJtLVHctFJm65hu6LttXDaCBTvII+/242h22Rr4TE2TEzEqR4ltAhjY8uFZLZUPoYYIgRxlhihQo1r5yRwzbaSvl4pfTFpqytrXfopyEP4JqhYDcjI62c74mahmB2TWv2FivnNMsFQ85SsZiQ/5GbEG96BrZuHEa8F5AEDiXhgrwhHHs8hGy9QaeBJB6/Br33csCVptfRR3wsvftP796BRkjoCe1XgV9T1cmp+z8sIvVc85U67v4XXp3C4+HcHiyTlP2O/QCKFNyL",id);
                    var CTPieDnetGcnRP_query;
                    CTPieDnetGcnRP_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.5$icpN5dRRYMN3rightC0M5R4R5R6R7R8RaRbRcRdSc$GuM1oInfJLRWR4C6S4$FACER6R7R8CbA1S11.9$F2ji2J7PGpLboRN_0opExtrudeRbSa$SWEPT_FACE",id);
                    var RKAPXCkveZOHRx_query;
                    RKAPXCkveZOHRx_query=qCompressed(1.0,"&21a$eJyNUUtzgjAQ/jPbow7hoXLkEZUqgSbYjl6YQFJNa4VCnOq/L0inl44zPe5+j9399sF34Oksm2vsMGSCUC3/KNT+zLWqTiHX3EOx+RfJrrVkyIKERouIeOs8xCkmISbBlrlQNWqvTvzYepPA6Iw5yJNW+tqrfA74t2A24HCBWQEH1eqqGZozCCj2sighHVDVsrmNjIRvQiQ8xBAaT2D+3LqlMCtGjqGxSnPjSzUyqbvpn/05NyMBbIWzYJljkkXZlklo36UuD8MCkWDl2Aaxc8yNmgs7sXZH+ar7jalNHTqhUzqjnBa0pD3VAlXWxBGUbmNi6aq+wxwhZzzqsrlrhDqYjxHsvbi5bA5uKOZLZNx1c38EI1SA+S+WRe1g0mXrJ+F2IAbFkJsLc/NNmY/TdFGvi4qS3KhqfNHNWUhadI9iLzjN8l74Db1FpsA=",id);
                    annotation { "Feature Name" : "Remove - Rail R" }
                    extrude(context, id + "FJfKTcHbn4RDfTj_1", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.REMOVE, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([qkOwlmqtAMnnBE_query, wAHzqXZUpPPpZD_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.BLIND, "oppositeDirection" : false, "depth" : { 'value' : try(lookup('bracket_rail_width') + lookup('bracket_rail_tolerance')), 'expression' : "#bracket_rail_width + #bracket_rail_tolerance" }.value, "endBoundEntityFace" : qUnion([]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : true, "startOffsetBound" : StartOffsetType.ENTITY, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([CTPieDnetGcnRP_query]), "symmetric" : false, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([RKAPXCkveZOHRx_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.FJfKTcHbn4RDfTj_1(id));
        features.FGQjFkE7PhMdSye_1 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var fVNBcSteJPzMuE_query;
                    fVNBcSteJPzMuE_query=qCompressed(1.0,"&2e3$eJyNUV1T2zAQ/DPXx2Qi4c9Hx1GIC7GTk2CgL4xsiaIpiY0t0rq/vooNLW1Dp2+n29vb3dOHuQ/bZ932a58TCsp0cleaz8/Smnq/kFYmZE3/RkTfaB6BKDbFZXF+60q9t8Ya3SVeQtPZOuAlKN2ag1bLtt65js/lONQfyXMJ7OeDe8AW58xRHkxn67Z/WZ8iS0RW5A6oG90OypmaU8hUQjgh0wCWcfd0SVehSWYtF9/uyFfT6qLhMTwdUw2LFPALJtLVHctFJm65hu6LttXDaCBTvII+/242h22Rr4TE2TEzEqR4ltAhjY8uFZLZUPoYYIgRxlhihQo1r5yRwzbaSvl4pfTFpqytrXfopyEP4JqhYDcjI62c74mahmB2TWv2FivnNMsFQ85SsZiQ/5GbEG96BrZuHEa8F5AEDiXhgrwhHHs8hGy9QaeBJB6/Br33csCVptfRR3wsvftP796BRkjoCe1XgV9T1cmp+z8sIvVc85U67v4XXp3C4+HcHiyTlP2O/QCKFNyL",id);
                    var whNrrJuAjKJUwy_query;
                    whNrrJuAjKJUwy_query=qCompressed(1.0,"&1ee$eJx1UMtygjAU/ZnbZR2C4mPJI9D4CDahdXDDBEI1VgmF2OrfF2Wmm47Le8/jnnuePAdez2VzXTkc2SBVK0652p2FUboKhBEuWtn/keRalxwNIWYkItRdZgFeYxpg6qd8BrpRO1WJY+s6vtUZCygro8z1pvIE4L+BjwAHEeY57FVrdNMvp+Az7CYkph2g67K5nyTSs4FIF3GEBmMI39tZIW3N6TGwFuvM+lFNGdfd9a/bO3cjCXyBE/8lwzQhScpLaD9LU+z7AETyojOSW8d+U6EcxcNtro3Rp1tmNmIOG7MJmzLBclYwyZ+RPRiC0fUDvBg4sLkEbDp0UDITtCthbx5xwY1U+C3TykfR4SEpOq+QJtXHfMk2bOSPu768OEh7np/3XcwgtA/Knk/WUb3MNaOZpWt8Mc1ZlizvyucbvE6ym/AXp5SeTw==",id);
                    annotation { "Feature Name" : "Extrude - Body" }
                    extrude(context, id + "FGQjFkE7PhMdSye_1", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.NEW, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([fVNBcSteJPzMuE_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.BLIND, "oppositeDirection" : false, "depth" : { 'value' : try(lookup('sp_width') + ((lookup('sp_tolerance') + lookup('bracket_rail_width')) * 2) - (lookup('bracket_rail_tolerance') * 1)), 'expression' : "#sp_width + ((#sp_tolerance+ #bracket_rail_width) *2) - (#bracket_rail_tolerance * 1)" }.value, "endBoundEntityFace" : qUnion([]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : false, "startOffsetBound" : StartOffsetType.BLIND, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([]), "symmetric" : true, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([whNrrJuAjKJUwy_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.FGQjFkE7PhMdSye_1(id));
        features.FwkvWZTtw4Xn7vL_2 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    const initialGuessFwkvWZTtw4Xn7vL_2 = { "qhlDsEgsCLdp.bottom" : [-3.469446951953614E-18, 0.045184999999999996, -1.0, 0.0, -0.04590999999999999, 0.04590999999999999], "qhlDsEgsCLdp.top" : [-3.469446951953614E-18, 0.042685, -1.0, 0.0, -0.04590999999999999, 0.04590999999999999], "qhlDsEgsCLdp.left" : [0.04590999999999999, 0.043935, 0.0, -1.0, -0.0012499999999999942, 0.0012500000000000011], "qhlDsEgsCLdp.right" : [-0.04591, 0.043935, 0.0, -1.0, -0.0012499999999999942, 0.0012500000000000011] };
                    {
                    }
                    var EtKsjpGZiMmHbJ_query;
                    EtKsjpGZiMmHbJ_query=qCompressed(1.0,"&1d0$eJx1kEFzgjAQhf9Meu0QBNRjDAGpBe2GdgYvTCAZyFSBQqj13xd0ppeOx7f77dt9+7Rx0duo+mvscWwjqQdxLnQ1CqPbxhdGEBzb/zvptVMcL9AeojBKyGvuswNLfJbQjK9R2+tKN+I0EIdascsFUo3R5jpPbQRif4I7iPkh4wWq9WDa/l5cIQqMpNE+mRptp/rbykhubBRJgjnGzx4KPoZ1Ke2WJyff2h1y66J7te+m7V9znJuRRHzHUrrNWZJGacYVGj6VKev7AZHk5WSkyy5xJUAWJ4uiNaY9zzeDAy54sIQVCCighAlGdPf+cgRrNI3qHkJVddK+2R43WuGHEAtqHIWU1Jc3DQ71pkcEhLKZ40ukB25EbwJY0eKed42C767tWFazepENoczxpH5MP0oFxfwxcshnh1+wEpck",id);
                    annotation { "Feature Name" : "Sketch - Cover Top" }
                    var sketch = newSketch(context, id + "FwkvWZTtw4Xn7vL_2", { "sketchPlane" : qUnion([EtKsjpGZiMmHbJ_query]), "disableImprinting" : false });
                    skLineSegment(sketch, "qhlDsEgsCLdp.bottom", { "construction" : false, "index" : "1" });
                    skLineSegment(sketch, "qhlDsEgsCLdp.top", { "construction" : false, "index" : "2" });
                    skLineSegment(sketch, "qhlDsEgsCLdp.left", { "construction" : false, "index" : "3" });
                    skLineSegment(sketch, "qhlDsEgsCLdp.right", { "construction" : false, "index" : "4" });
                    {
                        var nCnllxQnLnXHyb_query;
                        nCnllxQnLnXHyb_query=qCompressed(1.0,"%B5$QueryM6S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$8iArdl5WzThiC0M5R4R5R6R7R8RaRbRcRdSc$EFh1IGCAhwQiR4C6S6$VERTEXR6R7S7$isStartFR8CbA1S11.9$FvpopEYhEh3YsGd_1opExtrudeRbSa$CAP_VERTEX",id);
                        skConstraint(sketch, "qhlDsEgsCLdp.firstSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "1", "name" : "", "localFirst" : "qhlDsEgsCLdp.bottom.start", "externalSecond" : qUnion([nCnllxQnLnXHyb_query]) });
                    }
                    {
                        var xIONjHcHCKRvbM_query;
                        xIONjHcHCKRvbM_query=qCompressed(1.0,"%B5$QueryM6S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA2C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FVs9cd2oSNlD0KP_0wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$ggliDtHZBie1C0M5R4R5R6R7R8RaRbRcRdSc$ZW8XGf4wY7W4R4C6S6$VERTEXR6R7S7$isStartFR8CbA1S11.9$FvpopEYhEh3YsGd_1opExtrudeRbSa$CAP_VERTEX",id);
                        skConstraint(sketch, "qhlDsEgsCLdp.oppositeSnap0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "2", "name" : "", "localFirst" : "qhlDsEgsCLdp.top.end", "externalSecond" : qUnion([xIONjHcHCKRvbM_query]) });
                    }
                    {
                        skConstraint(sketch, "qhlDsEgsCLdp.perpendicular", { "constraintType" : ConstraintType.PERPENDICULAR, "index" : "1", "name" : "", "localFirst" : "qhlDsEgsCLdp.top", "localSecond" : "qhlDsEgsCLdp.left" });
                    }
                    {
                        skConstraint(sketch, "qhlDsEgsCLdp.parallel.1", { "constraintType" : ConstraintType.PARALLEL, "index" : "1", "name" : "", "localFirst" : "qhlDsEgsCLdp.bottom", "localSecond" : "qhlDsEgsCLdp.top" });
                    }
                    {
                        skConstraint(sketch, "qhlDsEgsCLdp.parallel.2", { "constraintType" : ConstraintType.PARALLEL, "index" : "2", "name" : "", "localFirst" : "qhlDsEgsCLdp.left", "localSecond" : "qhlDsEgsCLdp.right" });
                    }
                    {
                        skConstraint(sketch, "qhlDsEgsCLdp.horizontal", { "constraintType" : ConstraintType.HORIZONTAL, "index" : "1", "name" : "", "localFirst" : "qhlDsEgsCLdp.top" });
                    }
                    {
                        skConstraint(sketch, "qhlDsEgsCLdp.corner0", { "constraintType" : ConstraintType.COINCIDENT, "index" : "3", "name" : "", "localFirst" : "qhlDsEgsCLdp.bottom.start", "localSecond" : "qhlDsEgsCLdp.left.start" });
                    }
                    {
                        skConstraint(sketch, "qhlDsEgsCLdp.corner1", { "constraintType" : ConstraintType.COINCIDENT, "index" : "4", "name" : "", "localFirst" : "qhlDsEgsCLdp.bottom.end", "localSecond" : "qhlDsEgsCLdp.right.start" });
                    }
                    {
                        skConstraint(sketch, "qhlDsEgsCLdp.corner2", { "constraintType" : ConstraintType.COINCIDENT, "index" : "5", "name" : "", "localFirst" : "qhlDsEgsCLdp.top.start", "localSecond" : "qhlDsEgsCLdp.left.end" });
                    }
                    {
                        skConstraint(sketch, "qhlDsEgsCLdp.corner3", { "constraintType" : ConstraintType.COINCIDENT, "index" : "6", "name" : "", "localFirst" : "qhlDsEgsCLdp.top.end", "localSecond" : "qhlDsEgsCLdp.right.end" });
                    }
                    skSetInitialGuess(sketch, initialGuessFwkvWZTtw4Xn7vL_2);
                    skSolve(sketch);
                }
            };
        try(features.FwkvWZTtw4Xn7vL_2(id));
        features.FigI9as5Zfofmne_2 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var glKrGotuWjqEuw_query;
                    glKrGotuWjqEuw_query=qCompressed(1.0,"&35b$eJydkVtz2jAQhf+M+giD5Bs8GlukngJOV5o26UvGWDJoErAjKzD8+64NpM11On2yraPznT3rL9OAfH/S9rgIBGVEmbbYrsz6qXCm3qWFK2K6YG8VeWy0GBOZX+fz/OoWX/XOGWd0G4cxS0ZIWxGlrdlrNbP1tj8pTpeOnXlaEP78IXzC0yuOlo1pXW2PZ3wCPJZZvkShbrTtkzM1ZSRTMRWUDkMyO9zvf/6S7uDf7KL9/I4djNV5IybksWvVgxQR37hMvt7xpczkrdCkvdeu3JwGyJQoEfS4eUhbvm6TuWpWtXP1FgIIIYIxTJIS4wZqGBGzbazZOShFRLLFNWRLmdK+cAh+V/KPCVZQggItBtQbesTVDYy6ZQIFBl7MLi6g44sgApJDygGftcXtAUVeEomQ/OAg+c0ZTBmSqZcOTskB+DGax5/E++RBVw5Fqv5i4JJwfozjieyjnr3nBDx8Z8oPxweKziIdvc7pSVU3o/6PGGDV5cI/NA2INevNm6rAEF19HI1kYOxz3Zu8p1f9H/LJLE74S+03ppz7mg==",id);
                    var bZPPGsTpYjNqqA_query;
                    bZPPGsTpYjNqqA_query=qCompressed(1.0,"&1c2$eJx1kMtywjAMRX9GXbaDQ4CwzMNACuRhm3boJuNgF1weDomhpF/fhMx002Ep3SPdKz15A0gvsqyXA4osEKrix1xtL9wofQq44S5aWv8VVheSoj7EJJyGkbvIApzgKMCRv6Zj0KXaqhM/VK7t95rFHOTJKFO3Ux4H/FdQG3AwxTSHnaqMLrumAz7BLgvjqBF0Icu7ZSg8C0LhIorQyxAm4+q8sGYj5fZKym4Z+laljIvG/dyec18kgM4x82cZjljI1lRCtZdms+sChIJuoI5+VHJN42jGeJuV2GRAhmREHMJJTjakhVbSenNeySG3Pz8eQU2ka+qknB9WQs6TXBujjw/gZ2S/9MHogtj+sPmBFwfrDvHz7r4xTKbp12SPR8luKWgtM6QLfDPlRUiSNw+l7zhhWTv4C0XDki0=",id);
                    annotation { "Feature Name" : "Extrude - Cover Top" }
                    extrude(context, id + "FigI9as5Zfofmne_2", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.ADD, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([glKrGotuWjqEuw_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.BLIND, "oppositeDirection" : false, "depth" : { 'value' : try(lookup('asm_total_depth') - lookup('asm_sp_clearance')), 'expression' : "#asm_total_depth - #asm_sp_clearance " }.value, "endBoundEntityFace" : qUnion([]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : false, "startOffsetBound" : StartOffsetType.BLIND, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([]), "symmetric" : false, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([bZPPGsTpYjNqqA_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.FigI9as5Zfofmne_2(id));
        features.Fi1HqRA7guX7Fjb_2 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var YYloQLVGkwpxNI_query;
                    YYloQLVGkwpxNI_query=qCompressed(1.0,"%B5$QueryM6S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA1C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FwkvWZTtw4Xn7vL_2wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.6$qhlDsEgsCLdpbottomR4R5R6R7S7$isStartFR8CbA1S11.9$FigI9as5Zfofmne_2opExtrudeRbS8$CAP_EDGE",id);
                    annotation { "Feature Name" : "Fillet - Cover Top" }
                    fillet(context, id + "Fi1HqRA7guX7Fjb_2", { "filletType" : FilletType.EDGE, "entities" : qUnion([YYloQLVGkwpxNI_query]), "side1Face" : qUnion([]), "side2Face" : qUnion([]), "centerFaces" : qUnion([]), "tangentPropagation" : true, "blendControlType" : BlendControlType.RADIUS, "crossSection" : FilletCrossSection.CIRCULAR, "radius" : { 'value' : try(lookup('asm_face_fillets')), 'expression' : "#asm_face_fillets" }.value, "nonCircularRadius" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "width" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "rho" : { 'value' : try(0.5), 'expression' : "0.5" }.value, "magnitude" : { 'value' : try(0.5), 'expression' : "0.5" }.value, "defaultsChanged" : false, "isAsymmetric" : false, "otherRadius" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipAsymmetric" : false, "isPartial" : false, "startPartialType" : EndTypePartialFillet.PERCENTAGE, "startPartialOffset" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "startPartialEntity" : qUnion([]), "partialFirstEdgeTotalParameter" : { 'value' : try(0.01), 'expression' : "0.01" }.value, "partialOppositeParameter" : true, "useTrimmedFirstBound" : false, "secondBound" : false, "endPartialType" : EndTypePartialFillet.PERCENTAGE, "endPartialOffset" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endPartialEntity" : qUnion([]), "useTrimmedSecondBound" : false, "partialSecondEdgeTotalParameter" : { 'value' : try(0.99), 'expression' : "0.99" }.value, "isVariable" : false, "vertexSettings" : [], "pointOnEdgeSettings" : [], "smoothTransition" : false, "allowEdgeOverflow" : true, "keepEdges" : qUnion([]), "smoothCorners" : false, "smoothCornerExceptions" : qUnion([]) });
                }
            };
        try(features.Fi1HqRA7guX7Fjb_2(id));
        features.F9n2cSo8Jfr3jqK_2 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var WRJAYCiadcWmkJ_query;
                    WRJAYCiadcWmkJ_query=qCompressed(1.0,"&351$eJx9UV1z2jAQ/DPqYzJI8RePspGDJ8GGk5oJTxnb0jQiwQZbhtBfH2E3SdMCT5Ju9253dT9CFy061RxmLscESd3m60L/6nKj62qSm5ziGfkfEYeN4gES2Ty7z26X9qoqo41WLXUpiUYzjxdIqkbvlIybem0rLs8H0uHYHOaIfT64g9jkltmWZ92aujn8GR8BoyLJUgvUG9X0yokMCUokxRzjaw/F43Z7T6a+pqOGi7cnvNeNyjZ8jLbHVP0gifgdE9H0iaUiEUuuUPuiTPk8GEgkL1G5Wj6KZMdoHNYwOmYGDARuKOnTuODQm/50wQMfAhhDASVIULbZbCu/eHXjqIYt4NFZ3iH9ree7RZZORQ5u5HMPPTAQ7HFgRqUNdSWvfaTXm0ZXBkobI0kFA84iMbnCn17IJRkuumrppd2kyhNLwB8MHFgKHk/wX13HGvdRMpuDFQJcDMsDB7Dz/R8Al19fYdFz8nYnu0WwyPPXn1LdzYvamGH9J73uk5VeOZUM3pz6hFcg/5oF4tnihxMg5UX8rK4wkr7g3Z50TnhZAauTeL8/B8U0Yt+xd5uXAEE=",id);
                    var MkxMqACvEteIqC_query;
                    MkxMqACvEteIqC_query=qCompressed(1.0,"&25d$eJyFkVFvgjAUhf9M96ixKCKPgEUJKu62y+KTQdppNQKWIrJfP8TFbNHEl6a995zvnpu+uSZ6L4Wq5ybFBuKyiI8buS1jLbN0HOvYwXPjscPqXNARYtEymkWTVXMVqZZaisIxHcPrNbQN4kLJs+C+yo5tJb6J6qvZjRG5P+gAkfGENJadLHSm6l+8B8RhQbRoGlkuVDs54K6BAu5ginF3iHy7OM2MqSWdnqLsssaVVCLKqY1O161aEEc0JMybrsmCBWxFBSoOQie7W4CA0wRts6UW4O/LkGgwYQgWjMD2kmZMh3ctJI+5kqmGhFoomC8hWLBxB982hUF73l2wgQQ4iAbLNHcO+FwZ5cD9I8BGo8B9wOZLQmLZuV9iKtNq/0gYv45ASQjhZ+ym24t6FkG8JFSE94Pw+5J/xR9PCWB6VvOFvuOR/70f0Au8tg==",id);
                    var VfXzYjwjiQdpBJ_query;
                    VfXzYjwjiQdpBJ_query=qCompressed(1.0,"&21b$eJyNkctuwjAQRX/GXYJwSghZmjzAgiR07CKxQiG2ikvzwDGP/H1DqFArKrUby55775kZ+Wlio5ej1E1kM2whoeo036q3Y2pUWfipSQmOrEeFN5VkY8STZbJIpuv2KgujjJI1GRLLG7S0LRJSq5MUoS7zrpLeTM01PElRcH+wIQr8adBGdqo2pW6+8B4EhNMkboWykrrrTMXEQlQQzDDuj1Do1oeFNXMUGWjGLxt8VlomFXPR4bpVBxKIzQPuzTZBzClfM4nqvTTZ7jYAFSxD2QfZOy5ZTarDHGwYgQNjcL2sbdMTfQepvNKqMJAxB9FoCTTmPr4tCsPuvIdgCxkIkFeq41bhETNVnN+/GbDVOvCz3/sHITax3pOFzVb5IwGw/SchUmJOL2VxMq/rXwlge077ASHxgp/aJwlxqRg=",id);
                    var lCjkrRpefeWgTE_query;
                    lCjkrRpefeWgTE_query=qCompressed(1.0,"&34e$eJx9Ucty4jAQ/BntMZQl/DwKI4MD2HikzeOUMrZqUQWwsRUS5+sjzIbd7EJOkqZ7prtHP0YOyl5k0y0cjgkqVZtvV+rXS65VtRvnOqd4Qf5HRFdL7iORLtN5Onk0V7nTSivZUoeS0Fq4fIVK2aiDLKOm2pqKw/MTqTs2j3LEzg9uIzaeMNOyVq2umu73+BAYFXGaGKCqZdMrx+WIoLikmGM8cFEUtPs5mXqKWg0Xb0/4VTUyrXmA9sdU/aAS8RkT4fSJJSIWj1yi9lnqYn0yEJe8QNUmm78utYVFl4F1zAwYCAwp6dM4YNNhfzrgggc+BLCCAkqQpvltHRQTktwOo4QCtq7yuuRdLQ9ZmkxFDk7ocRfdMRDs4cQMCxPqphx4SG3rRu00FCZGnAgGnIVijM9WyHcqh9Z937TeTkxizxDwJwP7hoKD8Q3+q+1Y5B6KF0swQkBOGi7YgO2vewBc/FmFQa/pD4bokPlZnm9+lnK21FV91Wl0fy8etsFtpf3oglPj5h+nQFzAq08bQIoLODmv6apusaHPXkDvRvV+9u0EwPIi3n+ejSIasq/YB6sj/dY=",id);
                    var vdaDFgsENbwnXd_query;
                    vdaDFgsENbwnXd_query=qCompressed(1.0,"&2d3$eJyNkF9TozAUxb9M9rEdkkKBx5QGy9YCvcmu+uQAyWi624KQVvHTm1J1/1hnfLvJ+Z2bc/Jt5qH1XrX9yuOYIKm7Ylvqu31hdL2bF6ageEU+KqJvFA+QyPLsMru4saPaGW206qhLSeSsprxEUrX6oGTc1lt74/HiBPVH86xA7P3AXcTmF8xa7nVn6rZ/XR8BoyLJUivUjWqHlxM5IyiRFHOMx1MUh93DJVn4mjotF0+3+FG3Kmt4iB6OrYZFEvElE9HilqUiETdcoe6XMtX9KUAieYX69Fnnh3WWLkQBzrEzYCAwoWRo44FLJ4CdYfRgCj4EEEIJFUhQvLJBDutgXRS/f0i1zMvamFPnczB6TDZ64+5k8OTW4EU+n6KfDAS7PpFRZcuN5NhHetu0emegsnWSVDDgLBLzEf5KphF2xxNk6ubTGPHVlbjeht9rE8QWwG8EDiyCwzn+y3W84z5KVjnYIEAmbxEAu/9hQALA5btOnDP6H/+n6e7q3CiIN/slM2c3DD/nophG7F/tBT3O3NU=",id);
                    var rjcZschglwhter_query;
                    rjcZschglwhter_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA1C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FwkvWZTtw4Xn7vL_2wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.4$qhlDsEgsCLdpleftR4C6S4$FACER6R7R8CbA1S11.9$FigI9as5Zfofmne_2opExtrudeRbSa$SWEPT_FACE",id);
                    var vctROqewbOVdYh_query;
                    vctROqewbOVdYh_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA1C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FwkvWZTtw4Xn7vL_2wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.5$qhlDsEgsCLdprightR4C6S4$FACER6R7R8CbA1S11.9$FigI9as5Zfofmne_2opExtrudeRbSa$SWEPT_FACE",id);
                    var pkuHMiVIWbNAGR_query;
                    pkuHMiVIWbNAGR_query=qCompressed(1.0,"&1c2$eJx1kMtywjAMRX9GXbaDQ4CwzMNACuRhm3boJuNgF1weDomhpF/fhMx002Ep3SPdKz15A0gvsqyXA4osEKrix1xtL9wofQq44S5aWv8VVheSoj7EJJyGkbvIApzgKMCRv6Zj0KXaqhM/VK7t95rFHOTJKFO3Ux4H/FdQG3AwxTSHnaqMLrumAz7BLgvjqBF0Icu7ZSg8C0LhIorQyxAm4+q8sGYj5fZKym4Z+laljIvG/dyec18kgM4x82cZjljI1lRCtZdms+sChIJuoI5+VHJN42jGeJuV2GRAhmREHMJJTjakhVbSenNeySG3Pz8eQU2ka+qknB9WQs6TXBujjw/gZ2S/9MHogtj+sPmBFwfrDvHz7r4xTKbp12SPR8luKWgtM6QLfDPlRUiSNw+l7zhhWTv4C0XDki0=",id);
                    annotation { "Feature Name" : "Remove - Cover Interface" }
                    extrude(context, id + "F9n2cSo8Jfr3jqK_2", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.REMOVE, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([WRJAYCiadcWmkJ_query, MkxMqACvEteIqC_query, VfXzYjwjiQdpBJ_query, lCjkrRpefeWgTE_query, vdaDFgsENbwnXd_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.UP_TO_SURFACE, "oppositeDirection" : false, "depth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "endBoundEntityFace" : qUnion([rjcZschglwhter_query]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : true, "startOffsetBound" : StartOffsetType.ENTITY, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([vctROqewbOVdYh_query]), "symmetric" : true, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([pkuHMiVIWbNAGR_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.F9n2cSo8Jfr3jqK_2(id));
        features.Fi8q89JiVBNEYfw_2 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var XSgqCvYzGOBHRl_query;
                    XSgqCvYzGOBHRl_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA3C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$F9sqL2H7iA0rSTx_1wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$olQLwPt01TyQC0M5R5R6R7R8R9RbRcRdReSc$cjYXTIvEAFBoC0M5R5R6R7R8R9RbRcRdReSc$yNziPvQONHTaR5C7S4$FACER7R8R9CcA1S11.9$F9n2cSo8Jfr3jqK_2opExtrudeRcSa$SWEPT_FACER5R18R7R8R9CcA1S-17.7.9$booleanopBooleanRcS4$COPY",id);
                    var rVrQlipgWTZfPM_query;
                    rVrQlipgWTZfPM_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA1C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$FwkvWZTtw4Xn7vL_2wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc.3$qhlDsEgsCLdptopR4C6S4$FACER6R7R8CbA1S11.9$FigI9as5Zfofmne_2opExtrudeRbSa$SWEPT_FACE",id);
                    var zXghpTSqLKwlXI_query;
                    zXghpTSqLKwlXI_query=qCompressed(1.0,"&1c2$eJx1kMtywjAMRX9GXbaDQ4CwzMNACuRhm3boJuNgF1weDomhpF/fhMx002Ep3SPdKz15A0gvsqyXA4osEKrix1xtL9wofQq44S5aWv8VVheSoj7EJJyGkbvIApzgKMCRv6Zj0KXaqhM/VK7t95rFHOTJKFO3Ux4H/FdQG3AwxTSHnaqMLrumAz7BLgvjqBF0Icu7ZSg8C0LhIorQyxAm4+q8sGYj5fZKym4Z+laljIvG/dyec18kgM4x82cZjljI1lRCtZdms+sChIJuoI5+VHJN42jGeJuV2GRAhmREHMJJTjakhVbSenNeySG3Pz8eQU2ka+qknB9WQs6TXBujjw/gZ2S/9MHogtj+sPmBFwfrDvHz7r4xTKbp12SPR8luKWgtM6QLfDPlRUiSNw+l7zhhWTv4C0XDki0=",id);
                    annotation { "Feature Name" : "Remove - Cover Interface R." }
                    extrude(context, id + "Fi8q89JiVBNEYfw_2", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.REMOVE, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([XSgqCvYzGOBHRl_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.UP_TO_SURFACE, "oppositeDirection" : true, "depth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "endBoundEntityFace" : qUnion([rVrQlipgWTZfPM_query]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : false, "startOffsetBound" : StartOffsetType.BLIND, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([]), "symmetric" : false, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([zXghpTSqLKwlXI_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.Fi8q89JiVBNEYfw_2(id));
        features.FbebLmLyEJQj793_2 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var lIKkbJexhanZun_query;
                    lIKkbJexhanZun_query=qCompressed(1.0,"%B5$QueryM5Sb$derivedFromC0M6S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA3C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$F9sqL2H7iA0rSTx_1wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$olQLwPt01TyQC0M5R5R6R7R8R9RbRcRdReSc$cjYXTIvEAFBoC0M5R5R6R7R8R9RbRcRdReSc$yNziPvQONHTaR5C7S4$FACER7R8S7$isStartFR9CcA1S11.9$Fi8q89JiVBNEYfw_2opExtrudeRcS8$CAP_FACER5R18R7R8R9CcA1S-18.7.9$booleanopBooleanRcS4$COPY",id);
                    var LiJwQFLCAouCAf_query;
                    LiJwQFLCAouCAf_query=qCompressed(1.0,"&38d$eJx1kVt3ojAUhf9M5lGXiQryCBiUVrkkaWeYF1aEtKa1RiHV0l8/Ae1Mp9W3XL59zj77/PDGIH0VVbO06AqUopIHUQaVevEHyzGFCJSy5i8r+fjKtVTbKdfchUv0/Yc1O0HhEMQknIWRu8inOMHRFEd+Rh2gKvkot3xTu6OuMAdiq6VuWpXHAf57oSOApzNsvKxlrVV1epwAn2CXhXFkPtROVF3LsPQQCEsXUgj7Fgicer9Ac1u6g4qytxweZSXinem+bwfsCpWA3mLmz3McsZBlVID6WehifTIQlrQATfQuk0MaR3PGW69kTCxikwlxyIoUpCTCQHcC3U9uyGY1evh9DTKWDukk5XxzV4rbZKW0PsV6Ae7BcX8ItNqRsW+bDLx4mp0QvzjN54Bglj4Fz9hO1suSNiKHaoffdPVaClKYQOlPnLC8E8J2RQSZ2FicxIt4lpljF7gUtWsZDxaBBDrnZoHr47YZtYGsqeaVZsYaLExZE7yb5C3wSUQQ7LwhFJw5gobdYOfGZEhGrmHRP/Zc78PmueI3xeirgiDnEmdd5qz/uOHV9alNujgmegBZk16FiqfsFwsP2A08ReDgy9wfe+nBAgRysp84N/Lei3D2cMxRl4gRQP5piT1k922zx5VSG8G3auedDiaWMaDJImR/ANbvFMY=",id);
                    annotation { "Feature Name" : "Extrude - Cover interface" }
                    extrude(context, id + "FbebLmLyEJQj793_2", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.ADD, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([lIKkbJexhanZun_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.BLIND, "oppositeDirection" : false, "depth" : { 'value' : try(lookup('cover_if_extension')), 'expression' : "#cover_if_extension" }.value, "endBoundEntityFace" : qUnion([]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : false, "startOffsetBound" : StartOffsetType.BLIND, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([]), "symmetric" : false, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([LiJwQFLCAouCAf_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.FbebLmLyEJQj793_2(id));
        features.FqoHX3XVHpZf8LP_2 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var ByDYWVDbdgDIhz_query;
                    ByDYWVDbdgDIhz_query=qCompressed(1.0,"%B5$QueryM5S12$disambiguationDataA1M2S12$disambiguationTypeS13$ORIGINAL_DEPENDENCYS9$originalsA1C0M5Sa$entityTypeBa$EntityTypeS4$EDGESb$historyTypeS8$CREATIONSb$operationIdB2$IdA1S11.6$F9sqL2H7iA0rSTx_1wireOpS9$queryTypeSd$SKETCH_ENTITYSe$sketchEntityIdSc$Ue2V8JRlb4fZR4C6S4$FACER6R7R8CbA1S11.9$FGQjFkE7PhMdSye_1opExtrudeRbSa$SWEPT_FACE",id);
                    var rIPffPLMSCmdtW_query;
                    rIPffPLMSCmdtW_query=qCompressed(1.0,"&343$eJx1kd12ojAUhV8mc6nLRPm7BAxKq4AJ7SznhhVIWjO1ghBt6dNPADvTZZ27hHz77H02PzwDbE6ibtcmzQEXtTwLHtTlqz9ZGxQiwGXDXnP5fGJKloc5U8yFa/T9JW0rQeEUxCRchJG7yuY4wdEcR/6WOqCs5bM8sH3jzvrBDIiDkqrtVB4D+O+FzgCeL7DOspONKuvhow18gt00jCP9UFai7i1D7iEQchdSCMcmCJzmuEJLS7qTmqbvGXyTtYgr7X7sFuwHcUDvceovMxylYbqlAjQvQhW7IUDIaQHa6EMm500cLVPWZSUGMYlFbOKQnBSEE6GhB4Ee7Tuyz2dPv/4H6Ujnjb1hbP/AxX2Sl0oNtd6AR9AYT4EqK2L4lu7Ai+fbAfGLYT8HBIvN7+AFW8luzWkrMlhW+F3VJy5IoQulP3GSZr0Qdr+IIF1bGifxKl5s9bEvXIrGNXUGk0ACnYtZ4Pq4M6MWkA1VrFapjgYLPVYX7yZZB3wREQT7bAgFF46gab/YxZhMyczVLPrHXuZ9xrxM/KaYXSsIcm5x5m3OvOYmV2k/2xzBAgTSPtrOnXz0Irx9estQv4cWQPal+hEyxpZuPy/LvWCHsvKGg17GADRZhekfoWr9zw==",id);
                    annotation { "Feature Name" : "Extrude 2" }
                    extrude(context, id + "FqoHX3XVHpZf8LP_2", { "domain" : OperationDomain.MODEL, "bodyType" : ExtendedToolBodyType.SOLID, "operationType" : NewBodyOperationType.REMOVE, "surfaceOperationType" : NewSurfaceOperationType.NEW, "flatOperationType" : FlatOperationType.REMOVE, "entities" : qUnion([ByDYWVDbdgDIhz_query]), "surfaceEntities" : qUnion([]), "wallShape" : qUnion([]), "midplane" : false, "thickness1" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "flipWall" : false, "thickness2" : { 'value' : try(0 * millimeter), 'expression' : "0 mm" }.value, "thickness" : { 'value' : try(5 * millimeter), 'expression' : "5 mm" }.value, "endBound" : BoundingType.BLIND, "oppositeDirection" : true, "depth" : { 'value' : try(lookup('bracket_rail_tolerance')), 'expression' : "#bracket_rail_tolerance" }.value, "endBoundEntityFace" : qUnion([]), "endBoundEntityBody" : qUnion([]), "endBoundEntityVertex" : qUnion([]), "hasOffset" : false, "offsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "offsetOppositeDirection" : false, "hasExtrudeDirection" : false, "extrudeDirection" : qUnion([]), "startOffset" : false, "startOffsetBound" : StartOffsetType.BLIND, "startOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "startOffsetOppositeDirection" : false, "startOffsetEntity" : qUnion([]), "symmetric" : false, "hasDraft" : false, "draftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "draftPullDirection" : false, "hasSecondDirection" : false, "secondDirectionBound" : BoundingType.BLIND, "secondDirectionOppositeDirection" : true, "secondDirectionDepth" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionBoundEntityFace" : qUnion([]), "secondDirectionBoundEntityBody" : qUnion([]), "secondDirectionBoundEntityVertex" : qUnion([]), "hasSecondDirectionOffset" : false, "secondDirectionOffsetDistance" : { 'value' : try(25 * millimeter), 'expression' : "25 mm" }.value, "secondDirectionOffsetOppositeDirection" : false, "hasSecondDirectionDraft" : false, "secondDirectionDraftAngle" : { 'value' : try(3 * degree), 'expression' : "3 deg" }.value, "secondDirectionDraftPullDirection" : false, "defaultScope" : false, "booleanScope" : qUnion([rIPffPLMSCmdtW_query]), "defaultSurfaceScope" : true, "booleanSurfaceScope" : qUnion([]) });
                }
            };
        try(features.FqoHX3XVHpZf8LP_2(id));
        features.Frjs93I8lbJNhXS_2 = function(id)
            {
                annotation { 'unused' : true }
                var features = features;
                if (true)
                {
                    {
                    }
                    var hvbCeJfSEtFMlK_query;
                    hvbCeJfSEtFMlK_query=qCompressed(1.0,"&21a$eJyNUUtzgjAQ/jPbow7hoXLkEZUqgSbYjl6YQFJNa4VCnOq/L0inl44zPe5+j9399sF34Oksm2vsMGSCUC3/KNT+zLWqTiHX3EOx+RfJrrVkyIKERouIeOs8xCkmISbBlrlQNWqvTvzYepPA6Iw5yJNW+tqrfA74t2A24HCBWQEH1eqqGZozCCj2sighHVDVsrmNjIRvQiQ8xBAaT2D+3LqlMCtGjqGxSnPjSzUyqbvpn/05NyMBbIWzYJljkkXZlklo36UuD8MCkWDl2Aaxc8yNmgs7sXZH+ar7jalNHTqhUzqjnBa0pD3VAlXWxBGUbmNi6aq+wxwhZzzqsrlrhDqYjxHsvbi5bA5uKOZLZNx1c38EI1SA+S+WRe1g0mXrJ+F2IAbFkJsLc/NNmY/TdFGvi4qS3KhqfNHNWUhadI9iLzjN8l74Db1FpsA=",id);
                    var CbGFXIcfAhrkQH_query;
                    CbGFXIcfAhrkQH_query=qCompressed(1.0,"&343$eJx1kd12ojAUhV8mc6nLRPm7BAxKq4AJ7SznhhVIWjO1ghBt6dNPADvTZZ27hHz77H02PzwDbE6ibtcmzQEXtTwLHtTlqz9ZGxQiwGXDXnP5fGJKloc5U8yFa/T9JW0rQeEUxCRchJG7yuY4wdEcR/6WOqCs5bM8sH3jzvrBDIiDkqrtVB4D+O+FzgCeL7DOspONKuvhow18gt00jCP9UFai7i1D7iEQchdSCMcmCJzmuEJLS7qTmqbvGXyTtYgr7X7sFuwHcUDvceovMxylYbqlAjQvQhW7IUDIaQHa6EMm500cLVPWZSUGMYlFbOKQnBSEE6GhB4Ee7Tuyz2dPv/4H6Ujnjb1hbP/AxX2Sl0oNtd6AR9AYT4EqK2L4lu7Ai+fbAfGLYT8HBIvN7+AFW8luzWkrMlhW+F3VJy5IoQulP3GSZr0Qdr+IIF1bGifxKl5s9bEvXIrGNXUGk0ACnYtZ4Pq4M6MWkA1VrFapjgYLPVYX7yZZB3wREQT7bAgFF46gab/YxZhMyczVLPrHXuZ9xrxM/KaYXSsIcm5x5m3OvOYmV2k/2xzBAgTSPtrOnXz0Irx9estQv4cWQPal+hEyxpZuPy/LvWCHsvKGg17GADRZhekfoWr9zw==",id);
                    var HGfbrrnkgVQNqn_query;
                    HGfbrrnkgVQNqn_query=qCompressed(1.0,"&4e8$eJyVkl1zojAUhv9M9lLHhO9LxGipCpjQdumNEyRqthYU4ld//QasTrVrZ/aCmcB5c97nvJxfXQNMtrw8jk2agoyXYsezflm8e52xQSECmajYeyoWWyZFkfeYZC4co++V+LjmFGogJP7AD9zRtIcjHPRw4CXUAUUpFiJnq8rVm8YM8FwKeaxvdRnAlxeqA9wbYMWyFJUsytNHG3gEu7EfBqpQrHnZWPpZFwE/cyGFsG2CvlNtRujBEm6npPFhCvei5OFauW/qAZtGGaBDHHsPUxzEfpxQDqo3LmfLE4Cf0Rk4Bh8i2k3C4CFmNSsxiEksYhOHpGRGMsKV6ImjZ/uRrFJ9/npPpJB2E3vC2Oop48MoLaQ8xfoPcQsabQ3IYk0Mz1IZdMNecpJ4s9N8DugPJn/6b9iKluOMHvkUFmt8kOU242SmAqUvOIqnzUVY/yKCVGxxGIWjcJCoYxO44JVrNgyfGqIR3YXXVGfLFgf9/dvu5TWWe/13bu1GU3QZrgVNsFmuehVeVN4oO4P3XQ/fdIEz0BcL32GV8Tov5u85r9tciOsbyt+8ItIJ0m+iQmfvFtJUU7USP9R1sOJz+ZPCAGoll1KVkV3XqQVERSUrZVxLmSJUFm505rtJjGidy9VPPUFpo+t8U6O7e0TxkAxfWDdfHMq7orHIhv6hyHfyKfliehWwk6MZLezHean92Qyb/4TSW3FLQ21LbVJaFCvO8mLdPR3UrDrwwij5Tk5g5z58vM0TM9j2cubf4xL2xnYexXM3wMl8/8n1fza7yvxYVVYeD3zra+YGPA8J2dWQTrul6eoxgBrMADQa+fFfakZ6HQ==",id);
                    var OvTzZjFqDBEkpQ_query;
                    OvTzZjFqDBEkpQ_query=qCompressed(1.0,"&21a$eJyNUUtzgjAQ/jPbow7hoXLkEZUqgSbYjl6YQFJNa4VCnOq/L0inl44zPe5+j9399sF34Oksm2vsMGSCUC3/KNT+zLWqTiHX3EOx+RfJrrVkyIKERouIeOs8xCkmISbBlrlQNWqvTvzYepPA6Iw5yJNW+tqrfA74t2A24HCBWQEH1eqqGZozCCj2sighHVDVsrmNjIRvQiQ8xBAaT2D+3LqlMCtGjqGxSnPjSzUyqbvpn/05NyMBbIWzYJljkkXZlklo36UuD8MCkWDl2Aaxc8yNmgs7sXZH+ar7jalNHTqhUzqjnBa0pD3VAlXWxBGUbmNi6aq+wxwhZzzqsrlrhDqYjxHsvbi5bA5uKOZLZNx1c38EI1SA+S+WRe1g0mXrJ+F2IAbFkJsLc/NNmY/TdFGvi4qS3KhqfNHNWUhadI9iLzjN8l74Db1FpsA=",id);
                    var IkxvVqhcKBkZXp_query;
                    IkxvVqhcKBkZXp_query=qCompressed(1.0,"&343$eJx1kd12ojAUhV8mc6nLRPm7BAxKq4AJ7SznhhVIWjO1ghBt6dNPADvTZZ27hHz77H02PzwDbE6ibtcmzQEXtTwLHtTlqz9ZGxQiwGXDXnP5fGJKloc5U8yFa/T9JW0rQeEUxCRchJG7yuY4wdEcR/6WOqCs5bM8sH3jzvrBDIiDkqrtVB4D+O+FzgCeL7DOspONKuvhow18gt00jCP9UFai7i1D7iEQchdSCMcmCJzmuEJLS7qTmqbvGXyTtYgr7X7sFuwHcUDvceovMxylYbqlAjQvQhW7IUDIaQHa6EMm500cLVPWZSUGMYlFbOKQnBSEE6GhB4Ee7Tuyz2dPv/4H6Ujnjb1hbP/AxX2Sl0oNtd6AR9AYT4EqK2L4lu7Ai+fbAfGLYT8HBIvN7+AFW8luzWkrMlhW+F3VJy5IoQulP3GSZr0Qdr+IIF1bGifxKl5s9bEvXIrGNXUGk0ACnYtZ4Pq4M6MWkA1VrFapjgYLPVYX7yZZB3wREQT7bAgFF46gab/YxZhMyczVLPrHXuZ9xrxM/KaYXSsIcm5x5m3OvOYmV2k/2xzBAgTSPtrOnXz0Irx9estQv4cWQPal+hEyxpZuPy/LvWCHsvKGg17GADRZhekfoWr9zw==",id);
                    var SbYGCvHSwQIeVh_query;
                    SbYGCvHSwQIeVh_query=qCompressed(1.0,"&4e8$eJyVkl1zojAUhv9M9lLHhO9LxGipCpjQdumNEyRqthYU4ld//QasTrVrZ/aCmcB5c97nvJxfXQNMtrw8jk2agoyXYsezflm8e52xQSECmajYeyoWWyZFkfeYZC4co++V+LjmFGogJP7AD9zRtIcjHPRw4CXUAUUpFiJnq8rVm8YM8FwKeaxvdRnAlxeqA9wbYMWyFJUsytNHG3gEu7EfBqpQrHnZWPpZFwE/cyGFsG2CvlNtRujBEm6npPFhCvei5OFauW/qAZtGGaBDHHsPUxzEfpxQDqo3LmfLE4Cf0Rk4Bh8i2k3C4CFmNSsxiEksYhOHpGRGMsKV6ImjZ/uRrFJ9/npPpJB2E3vC2Oop48MoLaQ8xfoPcQsabQ3IYk0Mz1IZdMNecpJ4s9N8DugPJn/6b9iKluOMHvkUFmt8kOU242SmAqUvOIqnzUVY/yKCVGxxGIWjcJCoYxO44JVrNgyfGqIR3YXXVGfLFgf9/dvu5TWWe/13bu1GU3QZrgVNsFmuehVeVN4oO4P3XQ/fdIEz0BcL32GV8Tov5u85r9tciOsbyt+8ItIJ0m+iQmfvFtJUU7USP9R1sOJz+ZPCAGoll1KVkV3XqQVERSUrZVxLmSJUFm505rtJjGidy9VPPUFpo+t8U6O7e0TxkAxfWDdfHMq7orHIhv6hyHfyKfliehWwk6MZLezHean92Qyb/4TSW3FLQ21LbVJaFCvO8mLdPR3UrDrwwij5Tk5g5z58vM0TM9j2cubf4xL2xnYexXM3wMl8/8n1fza7yvxYVVYeD3zra+YGPA8J2dWQTrul6eoxgBrMADQa+fFfakZ6HQ==",id);
                    annotation { "Feature Name" : "Delete part 1" }
                    deleteBodies(context, id + "Frjs93I8lbJNhXS_2", { "compositePartOption" : CompositePartDeleteOptions.DELETE, "entities" : qUnion([hvbCeJfSEtFMlK_query, CbGFXIcfAhrkQH_query, HGfbrrnkgVQNqn_query]), "nonCompositeEntities" : qUnion([OvTzZjFqDBEkpQ_query, IkxvVqhcKBkZXp_query, SbYGCvHSwQIeVh_query]) });
                }
            };
        try(features.Frjs93I8lbJNhXS_2(id));
        return context;
    }, millimeter, {});
