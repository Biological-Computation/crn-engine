namespace FSharpIDD.Plots

open WebSharper

[<JavaScript>]    
type Plot =
|   Polyline of Polyline.Plot
|   Markers of Markers.Plot
|   Bars of Bars.Plot
|   Histogram of Histogram.Plot
|   Heatmap of Heatmap.Plot

namespace FSharpIDD

open WebSharper    

[<JavaScript>]
module Chart =        
    open FSharpIDD.Plots

    /// Holds the settings of a labelled axis 
    type LabelledAxisRecord = {
        /// either positions of labels (in case ticks length == labels length) or positions of named interval bounds 
        Ticks: float seq
        /// labels to present on an axis
        Labels: string seq
        /// tilt angle (in degrees) of each label. Positive angle - clockwise
        Angle: float
        /// Whether to show all labels regardless of the zoom level and visible rect
        ForceLabelsVisibility: bool
    }

    type Axis = 
    /// The axis is disabled (not visible)
    |   Hidden
    /// Numeric axis of automatically calculated and placed numerical ticks with values
    |   Numeric of ScientificNotationEnabled:bool
    /// Labelled axis. Uses array with string labels(lables[]) and array of numerical values (ticks[]), where these labels will be placed. Also has an angle parameter
    |   Labelled of LabelledAxisRecord
    
    /// Creates a labelled axis using the specified ticks and labels arrays, tilt angle
    let createTiltedLabelledAxis ticks labels angle = 
        Labelled {
            Ticks = ticks
            Labels = labels
            Angle = angle
            ForceLabelsVisibility = false
        }
    
    /// Creates a labelled axis using the specified ticks and labels arrays
    let createLabelledAxis ticks labels = 
        Labelled {
            Ticks = ticks
            Labels = labels
            Angle = 0.0
            ForceLabelsVisibility = false
        }
        
    /// Sets a tilt angle of an each label on a labelled axis
    let setLabelsAngle angle labelledAxis =
        match labelledAxis with
        |   Labelled labelledAxisRecord ->
                Labelled { labelledAxisRecord with Angle = angle}
        |   _ ->
           failwith "Force labels visibility can be set only on a labelled axis"

    /// Sets whether to force visibility of all labels on the axis or not
    let setLabelsVisibility forceLabelsVisibility labelledAxis =
        match labelledAxis with
        |   Labelled labelledAxisRecord ->
                Labelled { labelledAxisRecord with ForceLabelsVisibility = forceLabelsVisibility }
        |   _ ->
           failwith "Force labels visibility can be set only on a labelled axis"

    type GridLines =
    |   Disabled
    |   Enabled of strokeColour: Colour.Colour * lineWidthPX: float

    let DefaultGridLines = Enabled(Colour.DarkGrey, 1.0)

    type LegendVisibility =
    /// The legend is always visible (even if all of the plots are without names)
    |   Visible
    /// The legend is visible if some of the plots have their name set
    |   Automatic
    /// The legend is not visible
    |   Hidden

    type VisibleRegion =
    /// Fits the visible region so that all of the data is visible adding additional padding (blank visible area) in pixels
    |   Autofit of dataPaddingPx:int
    /// Explicit visible region in data coordinates
    |   Explicit of xmin:float * ymin:float * xmax:float * ymax:float

    /// Represents single chart that can be transformed later into the HTML IDD Chart    
    type Chart = {
        /// The width of the chart in pixels
        Width: int 
        /// The height of the chart in pixels
        Height: int
        /// The text that is centered and placed above the chart
        Title: string
        /// The text which describes the X axis
        Xlabel: string
        /// Which X axis to draw
        Xaxis: Axis
        /// The text which describes the Y axis        
        Ylabel: string
        /// Which Y axis to draw
        Yaxis: Axis
        /// The appearance of grid lines
        GridLines : GridLines
        /// A collection of plots (polyline, markers, bar charts, etc) to draw
        Plots: Plot list
        /// Whether the legend (list of plot names and their icons) is visible in the top-right part of the chart
        IsLegendEnabled: LegendVisibility
        /// Whether the chart visible area can be navigated with a mouse or touch gestures
        IsNavigationEnabled: bool
        /// Whether the plot coordinates of the point under the mouse are shown in the tooltip
        IsTooltipPlotCoordsEnabled: bool
        /// Which visible rectangle is displayed by the chart
        VisibleRegion : VisibleRegion
        /// Time delay in seconds between mouse over and tooltip appearance.
        /// None means that the default IDD tooltip settings are used
        TooltipDelay : float option
    }

    let Empty : Chart = {
        Width = 800
        Height = 600
        Title = null // null means not set
        Xlabel = null // null means not set
        Ylabel = null // null means not set
        Xaxis = Axis.Numeric true
        Yaxis = Axis.Numeric true
        GridLines = DefaultGridLines
        IsLegendEnabled = Automatic
        IsNavigationEnabled = true
        Plots = []
        VisibleRegion = VisibleRegion.Autofit 20
        IsTooltipPlotCoordsEnabled = true
        TooltipDelay = None // means not set
    }

    let addPolyline polyline chart = { chart with Plots = Polyline(polyline)::chart.Plots }

    let addMarkers markers chart = { chart with Plots = Markers(markers)::chart.Plots }

    let addBars bars chart = { chart with Plots = Bars(bars)::chart.Plots }

    let addHistogram histogram chart = { chart with Plots = Histogram(histogram)::chart.Plots }

    let addHeatmap heatmap chart = { chart with Plots = Heatmap(heatmap)::chart.Plots }

    /// Sets the textual title that will be placed above the charting area
    let setTitle title chart =  { chart with Title = title}

    /// Sets the size of the chart in pixels
    let setSize width height chart = { chart with Width = width; Height = height}

    /// Sets the X axis textual  label (placed below the X axis)
    let setXlabel label chart = { chart with Xlabel = label}

    /// Sets the mode of X axis
    let setXaxis axisMode chart = {chart with Xaxis = axisMode}

    /// Sets the visible region that is displayed by the chart
    let setVisibleRegion region chart = { chart with VisibleRegion = region}

    /// Set the Y axis textual label (placed to the left of Y axis)
    let setYlabel label chart = { chart with Ylabel = label}

    /// Sets the mode of Y axis
    let setYaxis axisMode chart = { chart with Yaxis = axisMode}

    /// Set the mode of grid lines appearance
    let setGridLines gridLines chart = {chart with GridLines = gridLines}
    
    /// Set the visibility of the plot legend floating in the top-right region of the chart
    let setLegendEnabled legendVisibility chart = { chart with IsLegendEnabled = legendVisibility }

    /// Set whether the chart can be navigated with a mouse or touch gestures
    let setNavigationEnabled isEnabled chart = { chart with IsNavigationEnabled = isEnabled }

    /// Sets whether the plot coordinates are shown in the tooltips of the chart
    let setIsTooltipPlotCoordsEnabled isEnabled chart = { chart with IsTooltipPlotCoordsEnabled = isEnabled }

    /// Sets duration of the tooltip delay in seconds
    let setTooltipDelay delay chart = { chart with TooltipDelay = Some delay }