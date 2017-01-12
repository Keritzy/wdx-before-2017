//
//  ViewController.swift
//  Trax
//
//  Created by WangDa on 12/17/15.
//  Copyright © 2015 dawang. All rights reserved.
//

import UIKit
import MapKit

class ViewController: UIViewController, MKMapViewDelegate, UIPopoverPresentationControllerDelegate{

    @IBOutlet weak var mapView: MKMapView! {
        didSet {
            mapView.mapType = .Hybrid
            mapView.delegate = self
        }
        
    }
    
    var gpxURL: NSURL?{
        didSet {
            clearWaypoints()
            if let url = gpxURL {
                GPX.parse(url) {
                    if let gpx = $0 {
                        self.handleWaypoints(gpx.waypoints)
                    }
                }
            }
        }
    }
    
    private func clearWaypoints(){
        if mapView?.annotations != nil {
            mapView.removeAnnotations(mapView.annotations as [MKAnnotation])
        }
    }
    
    private func handleWaypoints(waypoints: [GPX.Waypoint]){
        mapView.addAnnotations(waypoints)
        mapView.showAnnotations(waypoints, animated: true)
    }
    
    @IBAction func addWaypoint(sender: UILongPressGestureRecognizer) {
        
        if sender.state == UIGestureRecognizerState.Began {
            let coordinate = mapView.convertPoint(sender.locationInView(mapView), toCoordinateFromView: mapView)
            let waypoint = EditableWaypoint(latitude: coordinate.latitude, longitude: coordinate.longitude)
            waypoint.name = "I'm A NEW PIN"
            //waypoint.links.append(GPX.Link(href: "https://lh3.googleusercontent.com/TNUI2krkbc0112M6u4wdSz13oQ4Mc1_w_yV8AaOpk3T46HOWQE62gWNqPs922wWH5PEReQrG8ebx1epK0P6sE0ck8WQCIpmmzG-Ajz_yd0Gbz_0B76fl0V1l4MAFs8yC1bssOYXfCHiGpeYeMjtLudfaqTwaUrqwLdPhJWnwBY0TdDplTHs7J1jrgjcRT0v0J4iJJCciCW7yMONpDVaey5PIjs5iBzsmVZay7ID0qqqDEGAIZKH-dRMUlglrMBpXu5iOzBLzJXlnW2UrwACAFZYM_FiScz4MADqrzBBfEMQySqaN5B-OJpVXqT4XdQgz7YUC1aflXL0QPSYgBF0FbGYSyTJSxPY8CgDLN8NgpZHh0-h8Vo-uz0lPQVpUBTFE1hi7kpSXBp-IoPJ5WKDRSLSIZ76Z_nQLhxrEcX2SZ_BXcAvefB66yjFrhMktsbA_L04bbZflJAxHIET8fneVRwnEcEJ5wFn9x1eWtHX-ol3o41RA4yXts9jQXZHkxWaKor2-KIih5w8w_nZUetZwdzMouunIfwwy_hnm87iInNrhpk9Yki9W3zDSM34m7U4FgRllFQ=w668-h890-no"))
            mapView.addAnnotation(waypoint)
        }
    }
    
    private struct Constants {
        static let PartialTrackColor = UIColor.greenColor()
        static let FullTrackColor = UIColor.blueColor().colorWithAlphaComponent(0.5)
        static let TrackLineWidth: CGFloat = 3.0
        static let ZoomColldown = 1.5
        static let LeftCalloutFrame = CGRect(x: 0, y: 0, width: 59, height: 59)
        static let AnnotationViewReuseIdentifier = "waypoint"
        static let ShowImageSegue = "Show Image"
        static let EditWayPointSegue = "Edit Waypoint"
        static let EditWaypointPopoverWidth: CGFloat = 320
    }
    
    
    func mapView(mapView: MKMapView, viewForAnnotation annotation: MKAnnotation) -> MKAnnotationView? {
        var view = mapView.dequeueReusableAnnotationViewWithIdentifier(Constants.AnnotationViewReuseIdentifier)
        if view == nil {
            view = MKPinAnnotationView(annotation: annotation, reuseIdentifier: Constants.AnnotationViewReuseIdentifier)
            view!.canShowCallout = true
        } else {
            view!.annotation = annotation
        }
        
        view?.draggable = annotation is EditableWaypoint
        
        view?.leftCalloutAccessoryView = nil
        view?.rightCalloutAccessoryView = nil
        
        if let waypoint = annotation as? GPX.Waypoint {
            if waypoint.thumbnailURL != nil {
                view?.leftCalloutAccessoryView = UIButton(frame: Constants.LeftCalloutFrame)
            }
            if annotation is EditableWaypoint {
                view?.rightCalloutAccessoryView = UIButton(type: UIButtonType.DetailDisclosure)
            }
        }
        
        return view
    }
    
    
    func mapView(mapView: MKMapView, annotationView view: MKAnnotationView, calloutAccessoryControlTapped control: UIControl) {
        if (control as? UIButton)?.buttonType == UIButtonType.DetailDisclosure {
            // edit waypoint
            mapView.deselectAnnotation(view.annotation, animated: false)
            performSegueWithIdentifier(Constants.EditWayPointSegue, sender: view)
        } else if let waypoint = view.annotation as? GPX.Waypoint {
            if waypoint.imageURL != nil {
                performSegueWithIdentifier(Constants.ShowImageSegue, sender: view)
            }
        }
        
    }
    
    
    
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        if segue.identifier == Constants.ShowImageSegue {
            if let waypoint = (sender as? MKAnnotationView)?.annotation as? GPX.Waypoint {
                if let wivc = segue.destinationViewController.contentViewController as? WaypointViewController {
                    wivc.waypoint = waypoint
                }else if let ivc = segue.destinationViewController.contentViewController as? ImageViewController{
                    ivc.imageURL = waypoint.imageURL
                    ivc.title = waypoint.title
                }
            }
        } else if segue.identifier == Constants.EditWayPointSegue {
            if let waypoint = (sender as? MKAnnotationView)?.annotation as? EditableWaypoint {
                if let ewvc = segue.destinationViewController.contentViewController as? EditWaypointViewController {
                    if let ppc = ewvc.popoverPresentationController {
                        let coordinatePoint = mapView.convertCoordinate(waypoint.coordinate, toPointToView: mapView)
                        ppc.sourceRect = (sender as! MKAnnotationView).popoverSourceRectForCoordinatePoint(coordinatePoint)
                        let minimumSize = ewvc.view.systemLayoutSizeFittingSize(UILayoutFittingCompressedSize)
                        ewvc.preferredContentSize = CGSize(width: Constants.EditWaypointPopoverWidth, height: minimumSize.height)
                        ppc.delegate = self
                    }
                    ewvc.waypointToEdit = waypoint
                }
            }
        }
    }
    
    func adaptivePresentationStyleForPresentationController(controller: UIPresentationController) -> UIModalPresentationStyle {
        return UIModalPresentationStyle.OverFullScreen
    }
    
    func presentationController(controller: UIPresentationController, viewControllerForAdaptivePresentationStyle style: UIModalPresentationStyle) -> UIViewController? {
        let navcon = UINavigationController(rootViewController: controller.presentedViewController)
        let visualEffectView = UIVisualEffectView(effect: UIBlurEffect(style: .ExtraLight))
        visualEffectView.frame = navcon.view.bounds
        navcon.view.insertSubview(visualEffectView, atIndex: 0)
    
        return navcon
    }
    
    func mapView(mapView: MKMapView, didSelectAnnotationView view: MKAnnotationView) {
        if let waypoint = view.annotation as? GPX.Waypoint {
            if let url = waypoint.thumbnailURL {
                if view.leftCalloutAccessoryView == nil {
                    // a thumbnail must have been added since the annotation view was created
                    view.leftCalloutAccessoryView = UIButton(frame: Constants.LeftCalloutFrame)
                }
                if let thumbnailImageButton = view.leftCalloutAccessoryView as? UIButton {
                    if let imageData = NSData(contentsOfURL: url) { // blocks main thread!
                        if let image = UIImage(data: imageData){
                            thumbnailImageButton.setImage(image, forState: .Normal)
                        }
                    }
                }
            }
        }
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        let center = NSNotificationCenter.defaultCenter()
        let queue = NSOperationQueue.mainQueue()
        let appDelegate = UIApplication.sharedApplication().delegate
        
        center.addObserverForName(GPXURL.Notification, object: appDelegate, queue: queue) { notification in
            if let url = notification.userInfo?[GPXURL.Key] as? NSURL {
                self.gpxURL = url
            }
        }
        
        gpxURL = NSURL(string: "http://cs193p.stanford.edu/Vacation.gpx")
    }

}

// good to hanlde the situation with navigation controller
extension UIViewController {
    var contentViewController: UIViewController {
        if let navcon = self as? UINavigationController{
            return navcon.visibleViewController!
        } else {
            return self
        }
    }
}

extension MKAnnotationView {
    func popoverSourceRectForCoordinatePoint(coordinatePoint: CGPoint) -> CGRect {
        var popoverSourceRectCenter = coordinatePoint
        popoverSourceRectCenter.x -= frame.width / 2 - centerOffset.x - calloutOffset.x
        popoverSourceRectCenter.y -= frame.height / 2 - centerOffset.y - calloutOffset.y
        return CGRect(origin: popoverSourceRectCenter, size: frame.size)
    }
}
